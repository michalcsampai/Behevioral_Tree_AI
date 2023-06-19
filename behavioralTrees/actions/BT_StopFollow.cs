using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;
using A_StarPathfiding;

namespace BehaviorTree
{
    public class BT_StopFollow : BT_Node
    {
       
        public BT_StopFollow(BT_Tree tree) : base(tree) { }


        public override BT_NodeStatus Evaluate()
        {
            object O_pathfinder;
            object O_Config;
            bool found = false;

            found = behaviorTree.Blackboard.TryGetValue("Pathfinder", out O_pathfinder);
            found = behaviorTree.Blackboard.TryGetValue("ShipConfig", out O_Config);

            if (found)
            {
                PathfidingAgent pathfinder = (PathfidingAgent)O_pathfinder;      
                Ship shipConfig = (Ship)O_Config;




                shipConfig.currentSails = 0;
                shipConfig.heading = 0;

                pathfinder.StopPath();
                return nodeStatus = BT_NodeStatus.Success;

            }
            return nodeStatus = BT_NodeStatus.Fail;
        }
    }
}
