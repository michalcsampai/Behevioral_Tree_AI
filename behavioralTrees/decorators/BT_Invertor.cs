using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_Invertor : BT_Decorator
    {
        public BT_Invertor(BT_Tree tree, BT_Node child) : base(tree, child) { }


        public override BT_NodeStatus Evaluate()
        {


            switch (childNode.Evaluate())
            {
                case BT_NodeStatus.Success:
                    return nodeStatus = BT_NodeStatus.Fail;
                case BT_NodeStatus.Fail:
                    return nodeStatus = BT_NodeStatus.Success;
                case BT_NodeStatus.Running:
                    return nodeStatus = BT_NodeStatus.Running;
                default:
                    return nodeStatus = BT_NodeStatus.Running;
            }



        }

    }
}
