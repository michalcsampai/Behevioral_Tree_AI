using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class BT_Tree : MonoBehaviour
    {
        public bool startedBehavior;
        public BT_Node rootNode;

        public Dictionary<string, object> Blackboard { get; set; }


        private bool blackboardInitialized;
        public BT_Node GetRoot { get { return rootNode; } }



        protected void Awake()
        {
            Blackboard = new Dictionary<string, object>();
            blackboardInitialized = false;
            rootNode = SetupTree();
            
        }

        private void Update()
        {
            if (!blackboardInitialized)
            {
                blackboardInitialized = InitializeBlackBoard();
            }
            if (startedBehavior)
            {
                if (rootNode != null)
                {
                    UpdateBlackBoard();
                    rootNode.Evaluate();
                    
                }
            }
            
        }

        protected abstract BT_Node SetupTree();
        protected abstract bool InitializeBlackBoard();
        protected abstract bool UpdateBlackBoard();
    }
}
