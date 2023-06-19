/*
 * Exaple Tree used for AI ships to find and target ships in other teams 
 * 
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SailsOfGlory.Core;
using SailsOfGlory.FireSystem;

namespace BehaviorTree
{
    public class BT_ShipTree : BT_Tree
    {
        public GameManager gameManager;
        public int team;
        public Ship shipConfig;
        public ShipFireSystem shipFireSystem;
        public ShipMovement shipMovementSystem;
        public Vector3 position;

        protected override BT_Node SetupTree()
        {
            BT_Node root = new BT_Sequence(this, new BT_Node[]
            {
                new BT_FindEnemyList(this),
                new BT_RepeatUntilFail(this,
                    new BT_Sequence(this, new BT_Node[]
                    {
                        new BT_GetClosest(this),
                        new BT_Invertor(this,
                            new BT_Sequence(this, new BT_Node[]
                            {
                                new BT_Selector(this, new BT_Node[]
                                {
                                    new BT_IsInRange(this, "AgentPosition"),
                                    new BT_Invertor(this,
                                        new BT_Selector(this, new BT_Node[]
                                        {
                                            new BT_IsInRange(this, "TargetPoint"),
                                            new BT_FindPoint(this),

                                        })),
                                    new BT_Sequence(this, new BT_Node[]
                                    {
                                        new BT_SetWaypoint(this),
                                        new BT_FollowPath(this, "CurrentWaypoint")

                                    })
                                }),
                                new BT_Sequence(this, new BT_Node[]
                                {
                                    new BT_IsInRange(this, "AgentPosition"),
                                    new BT_SetAttackCourse(this),
                                    new BT_FireOnEnemy(this)
                                })


                            }))

                    }))




            }) ; 
            return root;
        }

        protected override bool InitializeBlackBoard()
        {
            Blackboard.Add("GameManager", gameManager);
            Blackboard.Add("TeamIndex", team);
            Blackboard.Add("AgentPosition", position);
            Blackboard.Add("FireSystem", shipFireSystem);
            Blackboard.Add("MovementSystem", shipMovementSystem);
            Blackboard.Add("ShipConfig", shipConfig);
            return true;
        }

        protected override bool UpdateBlackBoard()
        {
            Blackboard["AgentPosition"] = position;
            return true;
        }

    }
}
