using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A_StarPathfiding;

namespace BehaviorTree
{
    public class BT_GetPath : BT_Node
    {
        public BT_GetPath(BT_Tree tree) : base(tree) { }



        public override BT_NodeStatus Evaluate()
        {

            object O_point;
            object O_pathfinder;
            object O_agentPosition;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("TargetPoint", out O_point);
            found = behaviorTree.Blackboard.TryGetValue("Pathfinder", out O_pathfinder);
            found = behaviorTree.Blackboard.TryGetValue("AgentPosition", out O_agentPosition);
            if (found)
            {
                PathfidingAgent agent = (PathfidingAgent)O_pathfinder;
                Vector3 end = (Vector3)O_point;
                Vector3 start = (Vector3)O_agentPosition;
                agent.RequstNewPath(start, end);

                return nodeStatus = BT_NodeStatus.Success;
            }
            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}
