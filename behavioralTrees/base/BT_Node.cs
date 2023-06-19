using System.Collections;
using System.Collections.Generic;


namespace BehaviorTree
{
    public enum BT_NodeStatus
    {
        Success,
        Fail,
        Running
    };

    public class BT_Node
    {

        public BT_NodeStatus nodeStatus;
        public BT_Tree behaviorTree { get; set; }


        //Constructors

        public BT_Node(BT_Tree tree)
        {
            behaviorTree = tree;
        }

       

        public virtual BT_NodeStatus Evaluate() => BT_NodeStatus.Fail;

       
        
    }
}
