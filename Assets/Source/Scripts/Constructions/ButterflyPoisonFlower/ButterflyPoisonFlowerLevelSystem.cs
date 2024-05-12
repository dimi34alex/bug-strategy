using System;
using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    [Serializable]
    public class ButterflyPoisonFlowerLevelSystem : ConstructionLevelSystemBase<ButterflyPoisonFlowerLevel>
    {
        private readonly ButterflyPoisonFlowerPoisonFogProcessor _poisonFogProcessor;
        private readonly ButterflyPoisonFlowerAttackProcessor _attackProcessor;
        private readonly SphereCollider _attackZoneCollider;
        
        public ButterflyPoisonFlowerLevelSystem(ConstructionBase construction, ButterflyPoisonFlowerConfig config, 
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage, 
            ref SphereCollider attackZoneCollider, ref ButterflyPoisonFlowerAttackProcessor attackProcessor,
            ref ButterflyPoisonFlowerPoisonFogProcessor poisonFogProcessor) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
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