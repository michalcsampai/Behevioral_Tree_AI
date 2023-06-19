using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_StarPathfiding;
using SailsOfGlory.Core;
using VectorOperations;


namespace BehaviorTree
{
    public class BT_FollowPath : BT_Node
    {
        string pointTofollow;
        public BT_FollowPath(BT_Tree tree, string point) : base(tree) 
        {
            pointTofollow = point;
        }




        public override BT_NodeStatus Evaluate()
        {
            
            object O_point;
            object O_agentPosition;
            object O_movementSystem;
            object O_Config;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue(pointTofollow, out O_point);
            found = behaviorTree.Blackboard.TryGetValue("AgentPosition", out O_agentPosition);
            found = behaviorTree.Blackboard.TryGetValue("MovementSystem", out O_movementSystem);
            found = behaviorTree.Blackboard.TryGetValue("ShipConfig", out O_Config);

            if (found)
            {
                Vector3 targetPoint = (Vector3)O_point;
                ShipMovement movement = (ShipMovement)O_movementSystem;
                Ship shipConfig = (Ship)O_Config;
                Vector3 agentPosition = (Vector3)O_agentPosition;

                shipConfig.currentSails = shipConfig.maxSails;
                Vector3 DirectionToTarget = targetPoint - agentPosition;
                float angle = VectorUtilities.AngleBetweenVectors(movement.desiredDirection, DirectionToTarget);

                if ((angle - 0) > 0.2f)
                {
                    shipConfig.heading = -1;
                    return nodeStatus = BT_NodeStatus.Running;
                }
                if ((angle - 0) < 0.2f)
                {
                    shipConfig.heading = 1;
                    return nodeStatus = BT_NodeStatus.Running;
                }
                else
                {
                    shipConfig.heading = 0;
                    return nodeStatus = BT_NodeStatus.Running;
                }
                


            }

            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}
