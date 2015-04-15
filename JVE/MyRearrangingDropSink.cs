using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightIdeasSoftware;

namespace JVE
{
    public class MyRearrangingDropSink : RearrangingDropSink
    {
        public MyRearrangingDropSink(Action<JSNode,JSNode> onReorder): base(false)
        {

        }
        public override void RearrangeModels(ModelDropEventArgs args) {
            switch (args.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                    //this.ListView.MoveObjects(args.DropTargetIndex, args.SourceModels);
                    break;
                case DropTargetLocation.BelowItem:
                    //this.ListView.MoveObjects(args.DropTargetIndex + 1, args.SourceModels);
                    break;
                case DropTargetLocation.Background:
                    //this.ListView.AddObjects(args.SourceModels);
                    break;
                default:
                    return;
            }

            if (args.SourceListView != this.ListView)
            {
                args.SourceListView.RemoveObjects(args.SourceModels);
            }
        }
    
    }
}
