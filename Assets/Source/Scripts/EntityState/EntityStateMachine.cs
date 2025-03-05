using System.Collections.Generic;
using System.Linq;

namespace BugStrategy.EntityState
{
    public enum EntityStateID
    {
        Idle = 0,
        Move = 2,
        Build = 4,
        Attack = 6,
        ExtractionResource = 8,
        StorageResource = 10,
        SwitchProfession = 12,
        HideInConstruction = 14,
        Repair = 16
    };

    public class EntityStateMachine
    {
        private readonly Dictionary<EntityStateID, EntityStateBase> _states;
        public EntityStateID ActiveState { get; private set; }

        public EntityStateMachine(IEnumerable<EntityStateBase> states, EntityStateID activeState)
        {
            _states = states.ToDictionary(state => state.EntityStateID, state => state);
            ActiveState = activeState;
            _states[ActiveState].OnStateEnter();
        }

        public void SetState(EntityStateID entityState)
        {
            if (ActiveState == entityState)
                return;

            if (!_states.ContainsKey(entityState))
                throw new KeyNotFoundException();
       
            _states[ActiveState].OnStateExit();

            ActiveState = entityState;

            _states[ActiveState].OnStateEnter();
        }

        public void OnUpdate()
        {
            _states[ActiveState].OnUpdate();
        }
    }
}