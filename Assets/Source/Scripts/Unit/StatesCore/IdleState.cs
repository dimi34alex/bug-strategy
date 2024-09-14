using System;
using BugStrategy.EntityState;

namespace BugStrategy.Unit
{
    public class IdleState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Idle;
        
        public override event Action StateExecuted;
        
        public IdleState()
        {
            
        }

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateExit()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
    }
}