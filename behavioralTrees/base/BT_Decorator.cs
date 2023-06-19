using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_Decorator : BT_Node
    {
        public BT_Node childNode { get; set; }


        public BT_Decorator(BT_Tree tree, BT_Node child): base(tree)
        {
            childNode = child;
        }
    }
}
