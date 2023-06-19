using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;
using VectorOperations;

namespace BehaviorTree
{
    public class BT_SetAttackCourse : BT_Node
    {
        public BT_SetAttackCourse(BT_Tree tree) : base(tree) { }


        public override BT_NodeStatus Evaluate()
        {

            object O_enemyPos;
            object O_agentPosition;
            object O_movementSystem;
            object O_Config;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("TargetPosition", out O_enemyPos);
            found = behaviorTree.Blackboard.TryGetValue("AgentPosition", out O_agentPosition);
            found = behaviorTree.Blackboard.TryGetValue("MovementSystem", out O_movementSystem);
            found = behaviorTree.Blackboard.TryGetValue("ShipConfig", out O_Config);

            if (found)
            {
                Vector3 enemyPosition = (Vector3)O_enemyPos;
                ShipMovement movement = (ShipMovement)O_movementSystem;
                Ship shipConfig = (Ship)O_Config;
                Vector3 agentPosition = (Vector3)O_agentPosition;

                shipConfig.currentSails = shipConfig.maxSails;

                Vector3 DirectionToTarget = enemyPosition - agentPosition;

                float angleBetweenShips = VectorUtilities.AngleBetweenVectors(movement.currentDirection, DirectionToTarget);



                Vector3 rightAtackVector = VectorUtilities.RotateVector(movement.currentDirection, 90);
                Vector3 leftAtackVector = VectorUtilities.RotateVector(movement.currentDirection, -90);
               

                //If right side is closer
                if (angleBetweenShips > 0f)
                {
                    
                    if (VectorUtilities.AngleBetweenVectors(DirectionToTarget, rightAtackVector) > 0.5f)
                    {
                        shipConfig.heading = 1;
                        return nodeStatus = BT_NodeStatus.Running;
                    }
                    else if (VectorUtilities.AngleBetweenVectors(DirectionToTarget, rightAtackVector) < -0.5f)
                    {
                        shipConfig.heading = -1;
                        return nodeStatus = BT_NodeStatus.Running;
                    }
                    else
                    {
                        shipConfig.heading = 0;
                        return nodeStatus = BT_NodeStatus.Running;
                    }

                }
                //If left side is closer
                else if (angleBetweenShips <= 0f)
                {
                    if (VectorUtilities.AngleBetweenVectors(DirectionToTarget, leftAtackVector) > 0.5f)
                    {
                        shipConfig.heading = -1;
                        return nodeStatus = BT_NodeStatus.Running;
                    }
                    else if (VectorUtilities.AngleBetweenVectors(DirectionToTarget, leftAtackVector) < -0.5f)
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
               


            }

            return nodeStatus = BT_NodeStatus.Fail;

        }
    }
}
