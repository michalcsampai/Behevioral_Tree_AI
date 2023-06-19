using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;

namespace BehaviorTree
{
    public class BT_FindEnemyList : BT_Node
    {
        
        public BT_FindEnemyList(BT_Tree tree) : base(tree) { }

        public override BT_NodeStatus Evaluate()
        {
            if (behaviorTree.Blackboard.ContainsKey("EnemyList"))
            {
                return  BT_NodeStatus.Success;
            }

            bool found = false;

            object O_gameManager;
            object O_curTeamIndex;
            found = behaviorTree.Blackboard.TryGetValue("GameManager", out O_gameManager);
            found = behaviorTree.Blackboard.TryGetValue("TeamIndex", out O_curTeamIndex);

            if (found)
            {
                GameManager gameManager = (GameManager)O_gameManager;
                int curTeamIndex = (int)O_curTeamIndex;

                List<Agent> enemyList = new List<Agent>();

                for (int i = 0; i < gameManager.teamList.Count; i++)
                {
                    if (curTeamIndex != i)
                    {
                        enemyList =gameManager.teamList[i].ActiveAgents;
                        
                    }
                }
                
                behaviorTree.Blackboard.Add("EnemyList", enemyList);
                
                return BT_NodeStatus.Success;
            }
            else return BT_NodeStatus.Fail;
        }
    }
}
