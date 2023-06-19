using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_StarPathfiding;

namespace BehaviorTree
{
    public class BT_SetWaypoint : BT_Node
    {
        public BT_SetWaypoint(BT_Tree tree) : base(tree) { }



        public override BT_NodeStatus Evaluate()
        {
            bool isInBlackboard = false;

            object O_currentWaypoint;
            object O_newWaypoint;
            Vector3 currentWaypoint = Vector3.zero;
            


            if (behaviorTree.Blackboard.TryGetValue("CurrentWaypoint", out O_currentWaypoint))
            {
                isInBlackboard = true;
                currentWaypoint = (Vector3)O_currentWaypoint;
            }


            bool found = behaviorTree.Blackboard.TryGetValue("TargetPoint", out O_newWaypoint);

            if (found)
            {

                Vector3 newWaypoint = (Vector3)O_newWaypoint;

                if (isInBlackboard)
                {

                    if (currentWaypoint == newWaypoint)
                    {
                        return nodeStatus = BT_NodeStatus.Success;
                    }
                    else
                    {
                        behaviorTree.Blackboard["CurrentWaypoint"] = newWaypoint;
                        return nodeStatus = BT_NodeStatus.Success;
                    }
                }

                //else add first Waypoint
                else
                {
                    behaviorTree.Blackboard.Add("CurrentWaypoint", newWaypoint);
                    return nodeStatus = BT_NodeStatus.Success;
                }

            }
            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}
