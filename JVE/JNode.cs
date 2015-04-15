using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JVE
{
    public enum JSType { None, String, Number, Bool, Array, Object };
    public delegate void OnJSNodeItemsChanged(JSNode n);
    public delegate void OnJSNodeChanged(JSNode node,string newKey, object newValue);
    
    //key&value values change triggers OnChange handler.
    [Serializable]
    public class JSNode : IList<JSNode>//, IObservable<JSNode>
    {
        private JSType _vt;
        private  string _key;
        private object _v;
        public string Key {get {return _key;} set {
            if (OnChanged!=null && value != _key) OnChanged(this,value,null);
        } }
        public JSNode Parent;
        List<JSNode> Items;
        public static OnJSNodeItemsChanged OnItemsChanged;
        public static OnJSNodeChanged OnChanged;

        public object Value { get { return _v; } set { 
            if (OnChanged != null && !Object.Equals(value,_v)) OnChanged(this, null, value); 
        } }
        public JSType Type { get { return _vt; } }

        public JSNode(string key, object value)
        {
            _key = key; AssignValue(value);
        }

        public JSNode(string key, JSType type)
        {
            _key = key; _vt = type;
            if (type == JSType.Array || type == JSType.Object) { Items = new List<JSNode>(); _v = Items; }
            else if (type == JSType.Number) _v = 0;
            else if (type == JSType.Bool) _v = false;
            else if (type == JSType.String) _v = "";
        }

        void AssignValue(object v)
        {
            if (v is String) _vt = JSType.String;
            else if (v is Boolean) _vt = JSType.Bool;
            else if (v is Array)
            {
                _vt = JSType.Array;
                Items = new List<JSNode>();
                Array a = (Array)v;
                for (int i = 0; i < a.Length; i++)
                {
                    Items.Add(new JSNode(i.ToString(), a.GetValue(i)) { Parent = this });
                }
                _v = Items;
                return;
            }
            else if (v is Dictionary<string, object>)
            {
                _vt = JSType.Object;
                Items = new List<JSNode>();
                foreach (KeyValuePair<string, object> kv in (Dictionary<string, object>)v)
                {
                    Items.Add(new JSNode(kv.Key, kv.Value) { Parent = this });
                }
                _v = Items;
                return;
            }
            else if (v is List<JSNode>) { // copy nodes
                Items = new List<JSNode>();
                _v = Items;
                foreach (JSNode n in (List<JSNode>)v) {
                    Items.Add(n.Clone());
                }
                return;
            }
            else _vt = JSType.Number;
            _v = v;
        }

        public void ExchangeKeyValue(ref string key, ref object val){
            if (key != null) { string ts = _key; _key = key; key = ts; }
            if (val != null) { object tv = _v; AssignValue(val); val = tv; }
            if (OnChanged != null) OnChanged(this, null, null);
        }

        public int IndexOf(JSNode item)
        {
            return Items.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            ItemsChanged(this);
        }

        public JSNode this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(JSNode item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(JSNode[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(JSNode item)
        {
            bool r = Items.Remove(item);
            ItemsChanged(this);
            return r;
        }

        private void ItemsChanged(JSNode n)
        {
            if (OnItemsChanged != null) OnItemsChanged(n);
        }

        public IEnumerator<JSNode> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Insert(int index, JSNode item)
        {
            if (index < 0 || index >= Items.Count) Items.Add(item);
            else { 
                Items.Insert(index, item); 
            }
            item.Parent = this;
            ItemsChanged(this);
        }

        public void Add(JSNode item)
        {
            Items.Add(item);
            item.Parent = this;
            ItemsChanged(this);
        }

        public void AddWOEvent(JSNode item)
        {
            Items.Add(item);
            item.Parent = this;
        }

        override public string ToString() {
            return Key+" : "+_v.ToString();
        }

        public int Index { get {return Parent.IndexOf(this);} }

        public int FindIndex(Func<JSNode, bool> cc)
        { 
            int i = 0;
            foreach (JSNode n in Items) {
                if (cc(n)) return i;
                i++;
            }
            return -1;
        }

        public JSNode Find(Func<JSNode, bool> cc) 
        {
            int i = FindIndex(cc);
            return i>=0 ? Items[i] : null;
        }


        internal JSNode Clone()
        {
            JSNode nn = new JSNode(Key, Value);
            return nn;
        }
    }
}
