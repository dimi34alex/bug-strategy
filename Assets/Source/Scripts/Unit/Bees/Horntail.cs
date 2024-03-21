using System.Collections.Generic;
using Projectiles.Factory;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Horntail : BeeUnit
    {
        [SerializeField] private HorntailConfig config;
    
        [Inject] private ProjectileFactory _projectileFactory;
    
        protected override ProfessionBase CurrentProfession => _warriorProfession;
        public override UnitType UnitType => UnitType.Horntail;

        private HorntailProfession _warriorProfession;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
        }

        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            _healthStorage.SetValue(_healthStorage.Capacity);
            _warriorProfession = new HorntailProfession(this, config.InteractionRange, config.Cooldown, 
                config.AttackRange, config.Damage, config.DamageRadius, _projectileFactory);
        
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