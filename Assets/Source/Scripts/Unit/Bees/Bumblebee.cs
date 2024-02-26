using System.Collections.Generic;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.States;
using UnityEngine;

namespace Unit.Bees
{
    public class Bumblebee : BeeUnit
    {
        [SerializeField] private BeeMeleeWarriorConfig config;
        protected override ProfessionBase CurrentProfession => _warriorProfession;
        public override UnitType UnitType => UnitType.Bumblebee;

        private WarriorProfessionBase _warriorProfession;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
        }

        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _warriorProfession = new MeleeWarriorProfession(this, config.InteractionRange, config.Cooldown, config.AttackRange, config.Damage);
        
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _warriorProfession),
                new MoveState(this, _warriorProfession),
                new AttackState(this, _warriorProfession),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
        }
    }    
}
