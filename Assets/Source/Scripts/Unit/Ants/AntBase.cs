﻿using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using BugStrategy.Projectiles.Factory;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.OrderValidatorCore;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Ants
{
    public abstract class AntBase : AntUnit
    {
        [SerializeField] private AntUnitConfig config;
        [SerializeField] private GameObject resource;
        [SerializeField] private Animator animator;

        [Inject] private readonly ProjectileFactory _projectileFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public ProfessionType CurProfessionType => CurrentProfession.ProfessionType;
        public int CurProfessionRang => CurrentProfession.ProfessionRang;
        
        protected override OrderValidatorBase OrderValidator => CurrentProfession.OrderValidatorBase;
        
        //TODO: create internal ais for ants
        public override InternalAiBase InternalAi { get; protected set; }
        public AntProfessionBase CurrentProfession { get; private set; }
        public ProfessionType TargetProfessionType { get; private set; }
        public int TargetProfessionRang { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            
            _healthStorage = new FloatStorage(config.HealthPoints, config.HealthPoints);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            CurrentProfession.HandleUpdate(time);
        }

        public override void OnElementExtract()
        {
            base.OnElementExtract();
            
            resource.SetActive(false);
            _healthStorage.SetValue(_healthStorage.Capacity);
            SetProfession(config.DefaultProfession);

            var stateBases = new List<EntityStateBase>()
            {
                new AntIdleState(this),
                new AntMoveState(this),
                new AntBuildState(this),
                new AntResourceExtractionState(this),
                new AntStorageResourceState(this),
                new AntAttackState(this),
                new AntSwitchProfessionState(this)
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);
        }

        public void GiveOrderSwitchProfession(ITarget target, AntProfessionConfigBase config) 
            => GiveOrderSwitchProfession(target, config.ProfessionType, config.AntProfessionRang);
        
        public void GiveOrderSwitchProfession(ITarget target,
            ProfessionType newProfessionType, AntProfessionRang newProfessionRang)
        {
            if(newProfessionType == CurProfessionType && newProfessionRang.Rang == CurProfessionRang || 
               target.IsAnyNull())
            {
                Debug.LogWarning($"You try give order to set profession that already set ({newProfessionType == CurProfessionType}, {newProfessionRang.Rang == CurProfessionRang}) or Target is null ({target})");
                AutoGiveOrder(target);
                return;
            }

            TargetProfessionType = newProfessionType;
            TargetProfessionRang = newProfessionRang.Rang;
            
            HandleGiveOrder(target, UnitPathType.Switch_Profession);
        }

        public void SwitchProfession(AntProfessionConfigBase newProfession)
        {
            TrySetProfession(newProfession);
            // AutoGiveOrder(null);
        }
        
        /// <summary>
        /// Set new profession with checks, for set without any check use SetProfession(...)
        /// </summary>
        private void TrySetProfession(AntProfessionConfigBase newProfession)
        {
            if (!newProfession.Access.Contains(UnitType))
            {
                Debug.LogWarning($"You try set profession ({newProfession}) that un accessed to this unit ({UnitType})");
                return; 
            }
            if(newProfession.ProfessionType == CurProfessionType && newProfession.ProfessionRang == CurProfessionRang)
            {
                Debug.LogWarning($"You try set profession that already set ({CurProfessionType}:{CurProfessionRang})({newProfession})");
                return;
            }

            SetProfession(newProfession);
        }
        
        /// <summary>
        /// Set new profession without any checks, for set with check use TrySetProfession(...)
        /// </summary>
        private void SetProfession(AntProfessionConfigBase newProfession)
        {
            switch (newProfession.ProfessionType)
            {
                case (ProfessionType.Worker):
                    CurrentProfession = new AntWorkerProfession(this, newProfession as AntWorkerConfig, _teamsResourcesGlobalStorage,
                        resource);
                    break;
                case (ProfessionType.MeleeWarrior):
                    CurrentProfession = new AntMeleeWarriorProfession(this, newProfession as AntMeleeWarriorConfig);
                    break;
                case (ProfessionType.RangeWarrior):
                    CurrentProfession  = new AntRangeWarriorOrderValidator(this, newProfession as AntRangeWarriorConfig, _projectileFactory);
                    break;
                default: throw new NotImplementedException();
            }

            /*need because worker can have resource in the hands,
             so it is need for hide resource skin with profession changing*/
            resource.SetActive(false);
            animator.runtimeAnimatorController = newProfession.AnimatorController;
        }
    }
}