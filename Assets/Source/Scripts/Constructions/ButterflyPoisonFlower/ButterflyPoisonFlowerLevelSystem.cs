using System;
using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyPoisonFlower
{
    [Serializable]
    public class ButterflyPoisonFlowerLevelSystem : ConstructionLevelSystemBase<ButterflyPoisonFlowerLevel>
    {
        private readonly ButterflyPoisonFlowerPoisonFogProcessor _poisonFogProcessor;
        private readonly ButterflyPoisonFlowerAttackProcessor _attackProcessor;
        private readonly SphereCollider _attackZoneCollider;
        
        public ButterflyPoisonFlowerLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, ButterflyPoisonFlowerConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, 
            ref SphereCollider attackZoneCollider, ref ButterflyPoisonFlowerAttackProcessor attackProcessor,
            ref ButterflyPoisonFlowerPoisonFogProcessor poisonFogProcessor) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _attackZoneCollider = attackZoneCollider;
            _attackProcessor = attackProcessor;
            _poisonFogProcessor = poisonFogProcessor;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _attackProcessor.SetData(CurrentLevel.AttackCooldown, CurrentLevel.AttackDamage, CurrentLevel.DamageRadius);
            _attackZoneCollider.radius = CurrentLevel.AttackRadius;
            _poisonFogProcessor.SetData(CurrentLevel.FogExistTime, CurrentLevel.FogRadius, CurrentLevel.StaticFogRadius);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _attackProcessor.SetData(CurrentLevel.AttackCooldown, CurrentLevel.AttackDamage, CurrentLevel.DamageRadius);
            _attackZoneCollider.radius = CurrentLevel.AttackRadius;
            _poisonFogProcessor.SetData(CurrentLevel.FogExistTime, CurrentLevel.FogRadius, CurrentLevel.StaticFogRadius);
        }
    }
}