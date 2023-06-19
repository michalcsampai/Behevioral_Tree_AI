using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_Selector : BT_Composite
    {
        public BT_Selector(BT_Tree tree, BT_Node[] children) : base(tree, children) { }

        public override BT_NodeStatus Evaluate()
        {

            foreach (BT_Node node in childrenNodes)
            {
                switch (node.Evaluate())
                {
                    case BT_NodeStatus.Success:
                        nodeStatus = BT_NodeStatus.Success;
                        return nodeStatus;

                    case BT_NodeStatus.Fail:
                        continue;

                    case BT_NodeStatus.Running:
                        nodeStatus = BT_NodeStatus.Running;
                        return nodeStatus;
                    default:
                        continue;
                }
            }

            nodeStatus = BT_NodeStatus.Fail;
            return nodeStatus;
        }
    }
}
