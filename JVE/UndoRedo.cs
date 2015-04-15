using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVE
{
    public class Command
    {
        static Stack<Command> undoStack = new Stack<Command>();
        static Stack<Command> redoStack = new Stack<Command>();
        internal JSNode node;
        internal string commandName;
        internal virtual void Execute(bool clearRedo = true)
        {
            undoStack.Push(this);
            if (clearRedo) redoStack.Clear();
            Changed();
        }

        internal virtual void Undo()
        {
            throw new NotImplementedException();
        }

        override public string ToString()
        {
            return commandName + " of '" + node.Key+"'";
        }

        public static string NextUndoName
        {
            get
            {
                return undoStack.Count > 0 ? undoStack.Peek().ToString() : "";
            }
        }

        public static string NextRedoName
        {
            get { return redoStack.Count > 0 ? redoStack.Peek().ToString() : ""; }
        }

        public static Action OnChanged;
        
        public static bool ExecuteUndo()
        {
            if (undoStack.Count > 0)
            {
                Command c = undoStack.Pop();
                c.Undo();
                redoStack.Push(c);
                Changed();
            }
            return false;
        }

        private static void Changed()
        {
            if (OnChanged != null) OnChanged();
        }

        public static bool ExecuteRedo()
        {
            if (redoStack.Count > 0)
            {
                Command c = redoStack.Pop();
                c.Execute(false);
            }
            return false;
        }

        public static void Clear(){
            redoStack.Clear();
            undoStack.Clear();
            Changed();
        }
    }

    public class Command_ChangeNode : Command
    {
        string _Key;
        object _Value;
        public Command_ChangeNode(JSNode n, string newKey, object newValue) {
            commandName = "change";
            node = n;
            _Key = newKey; _Value = newValue;
            Execute();
        }

        internal override void Execute(bool clearRedo = false)
        {
            node.ExchangeKeyValue(ref _Key, ref _Value);
            base.Execute(clearRedo);
        }

        internal override void Undo()
        {
           node.ExchangeKeyValue(ref _Key, ref _Value);
        }
    }

    public class Command_DeleteNode : Command
    {
        int deletedIdx;
        public Command_DeleteNode(JSNode n) 
        {
            commandName = "deletion";
            node = n;
            Execute();
        }
        internal override void Execute(bool clearRedo = false)
        {
            deletedIdx = node.Index;
            node.Parent.RemoveAt(deletedIdx);
            base.Execute(clearRedo);
        }

        internal override void Undo()
        {
            node.Parent.Insert(deletedIdx, node);
        }
    }

    public class Command_InsertNode : Command
    { 
        int insertedIdx;
        JSNode parentNode;
        public Command_InsertNode(JSNode n, JSNode parent, int index) 
        {
            commandName = "insertion";
            node = n; parentNode = parent; insertedIdx = index;
            Execute();
        }
        internal override void Execute(bool clearRedo = false)
        {
            parentNode.Insert(insertedIdx,node);
            base.Execute(clearRedo);
        }

        internal override void Undo()
        {
            parentNode.RemoveAt(insertedIdx);
        }
    }

    public class Command_MoveNode : Command 
    {
        JSNode parentNode;
        int index;
        public Command_MoveNode(JSNode n, JSNode newParent, int insertionIndex) 
        {
            commandName = "move";
            node = n; parentNode = newParent;
            index = insertionIndex;
            Execute();
        }

        internal override void Execute(bool clearRedo = false)
        {
            SwapParent();
            base.Execute(clearRedo);
        }

        private void SwapParent()
        {
            JSNode pn = node.Parent;
            int pi = node.Index;
            pn.Remove(node);
            parentNode.Insert(index, node);
            parentNode = pn; index = pi;
        }

        internal override void Undo()
        {
            SwapParent();
        }
      
    }

}
