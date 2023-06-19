using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;

namespace BehaviorTree
{
    //Get Closest enemy and update His position;
    public class BT_GetClosest : BT_Node
    {
        private Vector3 currentPosition;
        private float closest = Mathf.Infinity;

        public BT_GetClosest(BT_Tree tree) : base(tree) { }


        public override BT_NodeStatus Evaluate()
        {
            GameObject closestEnemy = null;
            GameObject currClosest = null;
            ShipMovement currShipMovement = null;
            bool isInBlackboard = false;

            if (behaviorTree.Blackboard.ContainsKey("Target"))
            {
                isInBlackboard = true;
                currClosest = (GameObject)behaviorTree.Blackboard["Target"];
                currShipMovement = (ShipMovement)behaviorTree.Blackboard["TargetMovement"];

            }

            bool found = false;
            object O_enemyList;


            found = behaviorTree.Blackboard.TryGetValue("EnemyList", out O_enemyList);

            if (found)
            {
                List<Agent> enemyList = (List<Agent>)O_enemyList;

                
                foreach (Agent enemyAgent in enemyList)
                {
                    if(Vector3.Distance(currentPosition, enemyAgent.agent.transform.position) < closest)
                    {
                        closestEnemy = enemyAgent.agent;
                    }
                }
                
                if (closestEnemy != null)
                {
                    if (closestEnemy == currClosest)
                    {
                        behaviorTree.Blackboard["TargetPosition"] = currShipMovement.currentPosition;
                        return BT_NodeStatus.Success;
                    }

                    if (isInBlackboard == false)
                    {
                        ShipMovement localMov = closestEnemy.transform.GetChild(0).GetComponent<ShipMovement>();

                        behaviorTree.Blackboard.Add("Target", closestEnemy);                      
                        behaviorTree.Blackboard.Add("TargetMovement", localMov);
                        behaviorTree.Blackboard.Add("TargetPosition", localMov.currentPosition);
                        return BT_NodeStatus.Success;
                    }
                    else
                    {
                        ShipMovement localMov = closestEnemy.transform.GetChild(0).GetComponent<ShipMovement>();

                        behaviorTree.Blackboard["Target"] = closestEnemy;
                        behaviorTree.Blackboard["Target"] = localMov;
                        behaviorTree.Blackboard["TargetPosition"] = localMov.currentPosition;
                        return BT_NodeStatus.Success;
                    }
                }

                else return BT_NodeStatus.Fail;
            }
            return BT_NodeStatus.Fail;
        }
    }
}
