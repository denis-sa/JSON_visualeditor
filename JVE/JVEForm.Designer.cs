namespace JVE
{
    partial class JVEForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JVEForm));
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnOpenFile = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveNodeToTempl = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.Panel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miCutNode = new System.Windows.Forms.ToolStripMenuItem();
            this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAddString = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddBoolean = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miAddObject = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddArray = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewFile = new System.Windows.Forms.ToolStripButton();
            this.tools.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewFile,
            this.btnOpenFile,
            this.btnSave,
            this.btnSaveAs,
            this.toolStripSeparator1,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator2,
            this.btnSaveNodeToTempl});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(565, 39);
            this.tools.TabIndex = 0;
            this.tools.Text = "toolStrip1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.White;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(48, 36);
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.ButtonClick += new System.EventHandler(this.btnOpenFileClick);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 36);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = global::JVE.Properties.Resources.SaveAs;
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.White;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(36, 36);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.White;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(36, 36);
            this.btnUndo.Text = "Undo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.White;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(36, 36);
            this.btnRedo.Text = "Redo";
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSaveNodeToTempl
            // 
            this.btnSaveNodeToTempl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveNodeToTempl.Image = global::JVE.Properties.Resources.SaveTemplate;
            this.btnSaveNodeToTempl.ImageTransparentColor = System.Drawing.Color.White;
            this.btnSaveNodeToTempl.Name = "btnSaveNodeToTempl";
            this.btnSaveNodeToTempl.Size = new System.Drawing.Size(36, 36);
            this.btnSaveNodeToTempl.Text = "Save node to templates";
            this.btnSaveNodeToTempl.Click += new System.EventHandler(this.btnSaveNodeToTempl_Click);
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 39);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(565, 510);
            this.panel.TabIndex = 1;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCopy,
            this.miCutNode,
            this.miPaste,
            this.miDelete,
            this.toolStripMenuItem2,
            this.miAddString,
            this.miAddNumber,
            this.miAddBoolean,
            this.toolStripMenuItem1,
            this.miAddObject,
            this.miAddArray,
            this.miAddTemplate});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(160, 316);
            // 
            // miCopy
            // 
            this.miCopy.Image = global::JVE.Properties.Resources.copy;
            this.miCopy.Name = "miCopy";
            this.miCopy.Size = new System.Drawing.Size(159, 30);
            this.miCopy.Text = "Copy";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miCutNode
            // 
            this.miCutNode.Image = global::JVE.Properties.Resources.cut;
            this.miCutNode.Name = "miCutNode";
            this.miCutNode.Size = new System.Drawing.Size(159, 30);
            this.miCutNode.Text = "Cut";
            this.miCutNode.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // miPaste
            // 
            this.miPaste.Image = global::JVE.Properties.Resources.paste;
            this.miPaste.Name = "miPaste";
            this.miPaste.Size = new System.Drawing.Size(159, 30);
            this.miPaste.Text = "Paste";
            this.miPaste.Click += new System.EventHandler(this.miPaste_Click);
            // 
            // miDelete
            // 
            this.miDelete.Image = global::JVE.Properties.Resources.delete;
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(159, 30);
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // miAddString
            // 
            this.miAddString.Image = global::JVE.Properties.Resources._string;
            this.miAddString.Name = "miAddString";
            this.miAddString.Size = new System.Drawing.Size(159, 30);
            this.miAddString.Text = "Add String";
            this.miAddString.Click += new System.EventHandler(this.tsmi1_Click);
            // 
            // miAddNumber
            // 
            this.miAddNumber.Image = global::JVE.Properties.Resources.number;
            this.miAddNumber.Name = "miAddNumber";
            this.miAddNumber.Size = new System.Drawing.Size(159, 30);
            this.miAddNumber.Text = "Add Number";
            this.miAddNumber.Click += new System.EventHandler(this.tsmi2_Click);
            // 
            // miAddBoolean
            // 
            this.miAddBoolean.Image = global::JVE.Properties.Resources._bool;
            this.miAddBoolean.Name = "miAddBoolean";
            this.miAddBoolean.Size = new System.Drawing.Size(159, 30);
            this.miAddBoolean.Text = "Add Boolean";
            this.miAddBoolean.Click += new System.EventHandler(this.tsmi3_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // miAddObject
            // 
            this.miAddObject.Image = global::JVE.Properties.Resources._object;
            this.miAddObject.Name = "miAddObject";
            this.miAddObject.Size = new System.Drawing.Size(159, 30);
            this.miAddObject.Text = "Add Object";
            this.miAddObject.Click += new System.EventHandler(this.addObjectToolStripMenuItem_Click);
            // 
            // miAddArray
            // 
            this.miAddArray.Image = global::JVE.Properties.Resources.array;
            this.miAddArray.Name = "miAddArray";
            this.miAddArray.Size = new System.Drawing.Size(159, 30);
            this.miAddArray.Text = "Add Array";
            this.miAddArray.Click += new System.EventHandler(this.addArrayToolStripMenuItem_Click);
            // 
            // miAddTemplate
            // 
            this.miAddTemplate.Image = global::JVE.Properties.Resources.template;
            this.miAddTemplate.Name = "miAddTemplate";
            this.miAddTemplate.Size = new System.Drawing.Size(159, 30);
            this.miAddTemplate.Text = "Add Template";
            // 
            // btnNewFile
            // 
            this.btnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFile.Image")));
            this.btnNewFile.ImageTransparentColor = System.Drawing.Color.White;
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(36, 36);
            this.btnNewFile.Text = "New";
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 549);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.tools);
            this.Name = "Form1";
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripSplitButton btnOpenFile;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miCopy;
        private System.Windows.Forms.ToolStripMenuItem miPaste;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miAddString;
        private System.Windows.Forms.ToolStripMenuItem miAddNumber;
        private System.Windows.Forms.ToolStripMenuItem miAddBoolean;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miAddObject;
        private System.Windows.Forms.ToolStripMenuItem miAddArray;
        private System.Windows.Forms.ToolStripMenuItem miCutNode;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miAddTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveNodeToTempl;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnNewFile;
    }
}

