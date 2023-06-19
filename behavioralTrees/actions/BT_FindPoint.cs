using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.FireSystem;

namespace BehaviorTree
{
    public class BT_FindPoint : BT_Node
    {
       public BT_FindPoint (BT_Tree tree) : base(tree) { }



        public override BT_NodeStatus Evaluate()
        {
            bool isInBlackboard = false;
            if (behaviorTree.Blackboard.ContainsKey("TargetPoint"))
            {
                isInBlackboard = true;
            }
            object O_target;
            object O_pos;
            object O_fSystem;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("TargetPosition", out O_target);
            found = behaviorTree.Blackboard.TryGetValue("AgentPosition", out O_pos);
            found = behaviorTree.Blackboard.TryGetValue("FireSystem", out O_fSystem);

            if (found)
            {
                Vector3 targetPos = (Vector3)O_target;
                Vector3 position = (Vector3)O_pos;
                ShipFireSystem fireSystem = (ShipFireSystem)O_fSystem;

                Vector3 point = Vector3.MoveTowards(targetPos, position, (fireSystem.cannons.range-3));
                
                if (isInBlackboard == false)
                {
                    behaviorTree.Blackboard.Add("TargetPoint", point);
                    return nodeStatus = BT_NodeStatus.Success;
                    
                }
                else
                {
                    behaviorTree.Blackboard["TargetPoint"] = point;
                    return nodeStatus = BT_NodeStatus.Success;
                }
            }



            return nodeStatus = BT_NodeStatus.Fail;
        }





    }
}
