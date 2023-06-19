using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_Composite : BT_Node
    {
        public List<BT_Node> childrenNodes { get; set; }


        public BT_Composite(BT_Tree tree, BT_Node[] nodes) :base(tree)
        {
            childrenNodes = new List<BT_Node>(nodes);
        }

    }
}
