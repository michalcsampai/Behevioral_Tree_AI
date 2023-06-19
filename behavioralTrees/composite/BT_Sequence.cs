using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BT_Sequence : BT_Composite
    {
        public BT_Sequence(BT_Tree tree, BT_Node[] children) : base(tree,children) { }

        public override BT_NodeStatus Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BT_Node node in childrenNodes)
            {
                switch (node.Evaluate())
                {
                    case BT_NodeStatus.Fail:
                        nodeStatus = BT_NodeStatus.Fail;
                        return nodeStatus;

                    case BT_NodeStatus.Success:
                        continue;

                    case BT_NodeStatus.Running:
                        anyChildIsRunning = true;
                        continue;
                }
            }

            nodeStatus = anyChildIsRunning ? BT_NodeStatus.Running : BT_NodeStatus.Success;
            return nodeStatus;
        }

    }
}
