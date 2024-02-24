using System.Collections.Generic;
using Projectiles;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.States;
using UnityEngine;
using Zenject;

namespace Unit.Bees
{
    public class Wasp : BeeUnit
    {
        [SerializeField] private BeeRangeWarriorConfig config;
    
        [Inject] private ProjectilesPool _projectilesPool;
    
        public override IReadOnlyProfession CurrentProfession => _warriorProfession;
        public override UnitType UnitType => UnitType.Wasp;

        private WarriorProfessionBase _warriorProfession;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            _warriorProfession = new RangeWarriorProfession(this, config.InteractionRange, config.Cooldown, config.AttackRange, config.Damage, config.ProjectileType, _projectilesPool);
        
            var states = new List<EntityStateBase>()
            {
                new WarriorIdleState(this, _warriorProfession),
                new MoveState(this, _warriorProfession),
                new AttackState(this, _warriorProfession),
            };
            _stateMachine = new EntityStateMachine(states, EntityStateID.Idle);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        
            _warriorProfession.HandleUpdate(Time.deltaTime);
        }
    }
}
