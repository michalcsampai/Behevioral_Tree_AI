using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.FireSystem;

namespace BehaviorTree
{
    public class BT_IsInRange : BT_Node
    {
        string positionToEvaluate;
        public BT_IsInRange(BT_Tree tree, string pos) : base(tree) 
        {
            positionToEvaluate = pos;
        }

        public override BT_NodeStatus Evaluate()
        {

            object O_targetPosition;
            object O_fireSystem;
            object O_positionToEvaluate;

            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("TargetPosition", out  O_targetPosition);
            found = behaviorTree.Blackboard.TryGetValue("FireSystem", out O_fireSystem);

            found = behaviorTree.Blackboard.TryGetValue(positionToEvaluate, out O_positionToEvaluate);

            if (found)
            {
                Vector3 targetPosition = (Vector3)O_targetPosition;
                Vector3 positionEvaluate = (Vector3)O_positionToEvaluate;
                ShipFireSystem fire = (ShipFireSystem)O_fireSystem;



                if (Vector3.Distance(targetPosition, positionEvaluate) < fire.cannons.range)
                {
                    
                    return nodeStatus = BT_NodeStatus.Success;
                }
                else
                {
                    
                    return nodeStatus = BT_NodeStatus.Fail;
                }
            }
            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}