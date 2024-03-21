using System.Collections.Generic;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public sealed class Sawyer : BeeUnit
    {
        [SerializeField] private SawyerConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override ProfessionBase CurrentProfession => _warriorProfession;
        public override UnitType UnitType => UnitType.Sawyer;

        private RangeWarriorProfession _warriorProfession;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
        }

        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _warriorProfession = new RangeWarriorProfession(this, config.InteractionRange, config.Cooldown, 
                config.AttackRange, config.Damage, config.ProjectileType, _projectileFactory);
        
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