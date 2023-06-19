using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;
using SailsOfGlory.FireSystem;
using VectorOperations;

namespace BehaviorTree
{
    public class BT_FireOnEnemy : BT_Node
    {
       public BT_FireOnEnemy(BT_Tree tree) : base(tree) { }



        public override BT_NodeStatus Evaluate()
        {
            object O_fireSystem;
            object O_agentPosition;
            object O_movementSystem;
            object O_Config;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("FireSystem", out O_fireSystem);
            found = behaviorTree.Blackboard.TryGetValue("AgentPosition", out O_agentPosition);
            found = behaviorTree.Blackboard.TryGetValue("MovementSystem", out O_movementSystem);
            found = behaviorTree.Blackboard.TryGetValue("ShipConfig", out O_Config);

            if (found)
            {
                ShipFireSystem fireSystem = (ShipFireSystem)O_fireSystem;
                ShipMovement movement = (ShipMovement)O_movementSystem;
                Ship shipConfig = (Ship)O_Config;
                Vector3 agentPosition = (Vector3)O_agentPosition;

                //Debug.DrawRay(agentPosition, leftAtackVector, Color.magenta);
                //Debug.DrawRay(agentPosition, rightAtackVector, Color.black);
                

                if (fireSystem.readyToFireRight)
                {
                    Vector3 rightAtackVector = VectorUtilities.RotateVector(movement.currentDirection, 90);
                    Ray2D rightRay = new Ray2D(agentPosition, rightAtackVector);
                    RaycastHit2D rightHit = Physics2D.Raycast(rightRay.origin, rightRay.direction, fireSystem.cannons.range);

                    if ((rightHit.collider != null) && (fireSystem.readyToFireRight))
                    {

                        if (rightHit.transform.gameObject.GetComponent<Ship>().team != shipConfig.team)
                        {
                            Debug.DrawLine(rightRay.origin, rightHit.point, Color.black);
                            fireSystem.fireRightCannons();
                        }
                    }
                }
                
                if(fireSystem.readyToFireLeft)
                {
                    Vector3 leftAtackVector = VectorUtilities.RotateVector(movement.currentDirection, -90);
                    Ray2D leftRay = new Ray2D(agentPosition, leftAtackVector);
                    RaycastHit2D leftHit = Physics2D.Raycast(leftRay.origin, leftRay.direction, fireSystem.cannons.range);


                    if (leftHit.collider != null)
                    {

                        if (leftHit.transform.gameObject.GetComponent<Ship>().team != shipConfig.team)
                        {
                            Debug.DrawLine(leftRay.origin, leftHit.point, Color.black);
                            fireSystem.fireLeftCannons();
                        }

                    }
                }
                return nodeStatus = BT_NodeStatus.Success;
            }

                    
            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}
