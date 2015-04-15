using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using BrightIdeasSoftware;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Web;

namespace JVE
{
    public partial class JVEForm : Form
    {
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        JavaScriptSerializer jss;
        JSNode rootNode;
        JSNode templateNodes;
        TreeListView tv;
        StringBuilder sb;
        private readonly string formTitle = "JSON editor";
        string script;
        string appPath;
        List<string> recentFiles;
        public bool AvoidRefreshTVOnChange;
        private string activeFileName;
        private readonly string templatesFileName = "template.json";
        private readonly string recentFileName = "recent.txt";
        private bool IsChanged;

        public JVEForm()
        {
            InitializeComponent();
            this.Text = formTitle;
            this.appPath = System.AppDomain.CurrentDomain.BaseDirectory;
            JSNode.OnItemsChanged = delegate(JSNode n) {
                if (AvoidRefreshTVOnChange) return;
                if (n == rootNode) tv.SetObjects((IEnumerable)rootNode.Value, true);
                else tv.RefreshObject(n);
                IsChanged = true;
            };
            JSNode.OnChanged = delegate(JSNode node, string newKey, object newValue) {
                if(newKey!=null||newValue!=null) new Command_ChangeNode(node, newKey, newValue);
                tv.RefreshObject(node);
                IsChanged = true;
            };
            ofd = new OpenFileDialog() { DefaultExt = "JSON",Filter="*.json|*.json"};
            sfd = new SaveFileDialog() { DefaultExt = "JSON", Filter = "*.json|*.json" };
            jss = new JavaScriptSerializer();
            sb = new StringBuilder();
            Command.OnChanged = () => { btnUndo.Text = "Undo " + Command.NextUndoName;
                btnRedo.Text = "Redo " + Command.NextRedoName;
                btnUndo.Visible = false; btnUndo.Visible = true;
                btnRedo.Visible = false; btnRedo.Visible = true;
            };

            tv = new TreeListView() { Parent = this.panel, Dock = DockStyle.Fill, GridLines = true,
                                      CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick,
                                      HideSelection = false,
                                      IsSimpleDragSource=true, IsSimpleDropSink=true,
            };
            ((SimpleDropSink)tv.DropSink).CanDropBetween = true;
            ((SimpleDropSink)tv.DropSink).CanDropOnItem = false;
            tv.ModelCanDrop += (o, args) => {
                JSNode tn = args.TargetModel as JSNode;
                JSNode sn = args.SourceModels[0] as JSNode;
                args.Effect = DragDropEffects.None;
                if (tn == null) return;
                //this.Text = args.StandardDropActionFromKeys.ToString();
                if (args.StandardDropActionFromKeys == DragDropEffects.Move) {
                    while (tn != null) {
                        if (tn.Parent == sn) return;
                        tn = tn.Parent;
                    }
                }
                //if (args.DropTargetLocation == DropTargetLocation.Item && tn.Type != JSType.Object && tn.Type != JSType.Array) return; 
                args.Effect = args.StandardDropActionFromKeys;

            };
            tv.ModelDropped += (o, args) => {
                JSNode tn = args.TargetModel as JSNode;
                JSNode sn = args.SourceModels[0] as JSNode; 
                if (tn == null) return;
                int idx = tn.Index; if (args.DropTargetLocation == DropTargetLocation.BelowItem) idx++;
                AvoidRefreshTVOnChange = false;
                try
                {
                    if (args.Effect == DragDropEffects.Copy)
                    {
                        new Command_InsertNode(sn.Clone(), tn.Parent, idx);
                    }
                    else new Command_MoveNode(sn, tn.Parent, idx);
                    //Debug.WriteLine("SN:" + sn.ToString());
                    //Debug.WriteLine("TN:" + tn.Parent.ToString());
                }
                finally { AvoidRefreshTVOnChange = false; }
                //args.RefreshObjects();
            };

            tv.CanExpandGetter = delegate(object i) {
                return ((JSNode)i).Value is List<JSNode>; 
            };
            tv.ChildrenGetter = delegate(object i) {
                object v = ((JSNode)i).Value;
                //if (v is Array) return v;  
                return (IEnumerable<JSNode>)v; 
            };
            tv.UseCellFormatEvents = true;
            tv.FormatCell += delegate(object sender, FormatCellEventArgs e)
            {
                if (e.ColumnIndex != 0)
                {
                    JSType t = ((JSNode)e.Model).Type;
                    if ((t == JSType.Array || t == JSType.Object))
                    {
                        e.SubItem.BackColor = Color.Beige;
                    }
                }
            };
            tv.Columns.Add(new OLVColumn("Key", "Key") { MinimumWidth = 50, Width = 150 });
            tv.Columns.Add(new OLVColumn("Type", "Type") { MinimumWidth = 55 });
            tv.Columns.Add(new OLVColumn("Value", "Value")
            {
                MinimumWidth = 50,FillsFreeSpace=true,
                AspectToStringConverter = delegate(object i) { 
                    if (i is List<JSNode>) return null; else return i.ToString(); 
                }
            });
            tv.CellEditStarting += new CellEditEventHandler(HandleCellEditStarting);
            tv.CellRightClick += delegate(object sender, CellRightClickEventArgs e)
            {
                if (e.Model != null)
                {
                    tv.SelectObject(e.Model, true);
                }
                e.MenuStrip = contextMenu;
            };
            LoadTemplateNodes();
            LoadRecentList();
            NewJson();
        }

        private void LoadRecentList()
        {
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(appPath+recentFileName, FileMode.Open)))
                {
                    recentFiles = sr.ReadToEnd().Split(new string[]{"\r\n"},StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                }
                
            }
            catch {
                recentFiles = new List<string>();
            }
            UpdateRecentList();
        }

        private void OpenRecentClick(object sender, EventArgs e) {
            if (!CheckModified()) return;
            OpenJsonFile((string)((ToolStripDropDownItem)sender).ToolTipText);
        }

        private void AddFileToRecent(string FN) {
            recentFiles.Remove(FN);
            recentFiles.Insert(0, FN);
            if (recentFiles.Count > 10) recentFiles.Capacity = 10;

            using (StreamWriter sw = new StreamWriter(new FileStream(appPath + recentFileName, FileMode.Create)))
            {
                foreach (string i in recentFiles) sw.WriteLine(i);
            }
            UpdateRecentList();
        }

        private void UpdateRecentList()
        {
            btnOpenFile.DropDownItems.Clear();
            foreach (string i in recentFiles)
            {
                ToolStripDropDownItem di =(ToolStripDropDownItem)btnOpenFile.DropDownItems.Add(Path.GetFileName(i), null, OpenRecentClick);
                di.ToolTipText = i;
            }
        }

        private void LoadTemplateNodes()
        {
            templateNodes = LoadFile(appPath+templatesFileName, true);
            UpdateContextMenu();
        }

        private void AddTemplateClick(object sender, EventArgs e)
        {
            string key = ((ToolStripItem) sender).Text;
            JSNode tn = templateNodes.Find(sn => { return sn.Key == key; });
            if (tn!=null) AppendNodeToSelection(tn);
        }

        private void UpdateContextMenu()
        {
            //bool canHaveChildren = n.Type == JSType.Array || n.Type == JSType.Object;
            miAddTemplate.DropDownItems.Clear();
            foreach (JSNode n in templateNodes) {
                miAddTemplate.DropDownItems.Add(n.Key,null,AddTemplateClick);
            }
        }

        private void HandleCellEditStarting(object sender, CellEditEventArgs e)
        {
            JSType t = ((JSNode)e.RowObject).Type;
            e.Cancel = (t == JSType.Object || t == JSType.Array) && e.Column.Index!=0;
        }

        private void OpenJsonFile(string FN)
        {
                rootNode = LoadFile(FN, false);
                ResetView();
                activeFileName = FN;
                SetFormTitle();
                AddFileToRecent(activeFileName);

        }

        private void ResetView()
        {
            IsChanged = false;
            tv.SetObjects((IEnumerable)rootNode.Value);
            Command.Clear();
            tv.ExpandAll();
        }

        private JSNode LoadFile(string fileName, bool ignoreErrors)
        {
            JSNode root = new JSNode("root",JSType.Object);
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open), Encoding.UTF8))
                {
                    string data = sr.ReadToEnd();
                    int stp = data.IndexOf("(function()");
                    if (stp > 0) { script = data.Substring(stp); data = data.Remove(stp); }
                    else { script = ""; }
                    Dictionary<string, object> d = (Dictionary<string, object>)jss.DeserializeObject(data);
                    foreach (KeyValuePair<string, object> k in d)
                    {
                        root.AddWOEvent(new JSNode(k.Key, k.Value) { Parent = root });
                    }
                }
            }
            catch {
                if (!ignoreErrors) throw;
            }
            return root;
        }

        private void SetFormTitle()
        {
            this.Text = formTitle + " [" + activeFileName + "]";
        }

        public bool SaveFile(List<JSNode> nodes, string FN, bool addScript=false) { 
          using(StreamWriter sw = new StreamWriter(new FileStream(FN,FileMode.Create)))
          {
              string s = SerializeToJson(nodes);
              if (addScript) s += script;
              sw.WriteLine(s);
          }
          return true;
        }

        private string SerializeToJson(List<JSNode> nodes)
        {
            sb.Clear();
            sb.Append('{');
            SerializeNodes(nodes, false);
            sb.Append('}');
            return sb.ToString();
        }

        private void SerializeNodes(List<JSNode> jl, bool isArrayElem)
        {
            int l = jl.Count-1;
            JSNode n;
            for(int i=0;i<=l;i++)
            {
                n = jl[i];
                if (!isArrayElem)
                {
                    sb.Append('"');
                    sb.Append(HttpUtility.JavaScriptStringEncode(n.Key));
                    sb.Append("\":");
                }
                switch(n.Type) {
                  case JSType.String:
                        sb.Append('"');
                        sb.Append(HttpUtility.JavaScriptStringEncode((string)n.Value));
                        sb.Append('"');
                      break;
                  case JSType.Number:
                      sb.Append(n.Value.ToString());
                      break;
                  case JSType.Bool:
                      sb.Append((bool)n.Value ? "true" : "false");
                      break;
                  case JSType.Array:
                      sb.Append('[');
                      SerializeNodes((List<JSNode>)n.Value, true);
                      sb.Append(']');
                      break;
                  case JSType.Object:
                      sb.Append('{');
                      SerializeNodes((List<JSNode>)n.Value, false);
                      sb.Append('}');
                      break;
              }
              if(i<l) sb.Append(',');
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private bool SaveCurrentFile()
        {
            if (activeFileName != null) return SaveFile((List<JSNode>)rootNode.Value, activeFileName, true);
            else return SaveFileAs();
        }

        private void tsmi1_Click(object sender, EventArgs e)
        {
            AddNode(JSType.String);
        }

        private void tsmi2_Click(object sender, EventArgs e)
        {
            AddNode(JSType.Number);
        }

        private void tsmi3_Click(object sender, EventArgs e)
        {
            AddNode(JSType.Bool);
        }

        private void addObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode(JSType.Object);
        }

        private void addArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode(JSType.Array);
        }


        private void AddNode(JSType type)
        {
            JSNode nn = new JSNode("New " + type.ToString(), type);
            AppendNodeToSelection(nn);
        }

        private void AppendNodeToSelection(JSNode nn)
        {
            JSNode n = (JSNode)tv.SelectedObject;
            if (n == null) n=rootNode;
            if (n.Type == JSType.Array || n.Type == JSType.Object) //append to end (top?)
            {
                new Command_InsertNode(nn, n, n.Count);
            } else new Command_InsertNode(nn, n.Parent, n.Index);
            if (n.Type == JSType.Array || n.Type == JSType.Object) tv.Expand(n);
            tv.EnsureModelVisible(nn);
            tv.SelectObject(nn, true); //edit key?
            
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            DeleteFocusedNode();
        }

        private void DeleteFocusedNode()
        {
            JSNode n = (JSNode)tv.SelectedObject;
            if (n == null) return;
            new Command_DeleteNode(n);      
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            CopyFocusedNode();
        }

        private void CopyFocusedNode()
        {
            JSNode n = (JSNode)tv.SelectedObject;
            if (n == null) return;
            Clipboard.SetDataObject(n);
        }

        private void miPaste_Click(object sender, EventArgs e)
        {
            IDataObject cd = Clipboard.GetDataObject();
            JSNode o = null;
            if (cd.GetDataPresent(typeof(JSNode))) o = (JSNode)cd.GetData(typeof(JSNode));
            if (o !=null) AppendNodeToSelection(o);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutFocusedNode();
        }

        private void CutFocusedNode()
        {
            CopyFocusedNode();
            DeleteFocusedNode();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private bool SaveFileAs()
        {
            if (sfd.ShowDialog() == DialogResult.OK) {
                SaveFile((List<JSNode>)rootNode.Value, sfd.FileName,true);
                activeFileName = sfd.FileName;
                AddFileToRecent(activeFileName);
                SetFormTitle();
                return true;
            }
            return false;
        }

        private void btnSaveNodeToTempl_Click(object sender, EventArgs e)
        {
            AddSelectionToTemplates();
        }

        private void AddSelectionToTemplates()
        {
            JSNode n = (JSNode)tv.SelectedObject;
            if (n == null) return;
            int eni = templateNodes.FindIndex(sn => { return sn.Key == n.Key; });
            if (eni < 0) templateNodes.Add(n);
            else {
                if(DialogResult.Yes == MessageBox.Show("Template with such name already exists!\nOverwrite?",
                    "Key name conflict", MessageBoxButtons.YesNo))
                {
                    templateNodes[eni] = n;
                } else return;
            }
            SaveFile((List<JSNode>)templateNodes.Value, appPath+templatesFileName);
            UpdateContextMenu();
        }

        private void btnOpenFileClick(object sender, EventArgs e)
        {
            if (!CheckModified()) return;
            if (ofd.ShowDialog() == DialogResult.OK) {
              OpenJsonFile(ofd.FileName);
           }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            tv.CancelCellEdit();
            Command.ExecuteUndo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void Redo()
        {
            tv.CancelCellEdit();
            Command.ExecuteRedo();
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            NewJson();
        }

        private void NewJson()
        {
            if (!CheckModified()) return;
            activeFileName = null;
            SetFormTitle();
            rootNode = new JSNode("root", JSType.Object);
            ResetView();
        }

        private bool CheckModified() //false cancels calling operation
        {
            if (!IsChanged) return true;
            DialogResult dr = MessageBox.Show("Save changes?", "Document has unsaved changes", MessageBoxButtons.YesNoCancel);
            if(dr == DialogResult.Yes)
            {
                return SaveCurrentFile();
            }
            return dr == DialogResult.No;
        }

    }

}
