﻿using System.Collections.Generic;
using System.Linq;

namespace BugStrategy.EntityState
{
    public enum EntityStateID
    {
        Idle,
        Move,
        Build,
        Attack,
        ExtractionResource,
        StorageResource,
        SwitchProfession,
        HideInConstruction
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