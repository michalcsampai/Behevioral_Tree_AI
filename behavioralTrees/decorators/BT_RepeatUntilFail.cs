using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_RepeatUntilFail : BT_Decorator
    {
        public BT_RepeatUntilFail(BT_Tree tree, BT_Node child) : base(tree, child) { }



        public override BT_NodeStatus Evaluate()
        {
            nodeStatus = childNode.Evaluate();
            if (nodeStatus == BT_NodeStatus.Fail)
            {
                return nodeStatus = BT_NodeStatus.Success;
            }
            return nodeStatus = BT_NodeStatus.Running;

        }

    }
    
}
