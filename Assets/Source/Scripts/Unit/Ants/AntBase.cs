using System;
using System.Collections.Generic;
using System.Linq;
using Projectiles;
using Unit.Ants.ProfessionsConfigs;
using Unit.Ants.States;
using Unit.Professions;
using Unit.Professions.Ants;
using UnityEngine;
using Zenject;

namespace Unit.Ants
{
    public abstract class AntBase : AntUnit
    {
        [SerializeField] private AntProfessionConfigBase defaultProfessionConfig;
        [SerializeField] private GameObject resource;
        [SerializeField] private Animator animator;

        [Inject] private ProjectilesPool _projectilesPool;
        
        public ProfessionType CurProfessionType => CurrentProfession.ProfessionType;
        public override IReadOnlyProfession CurrentProfession => Profession;
        public int CurProfessionRang => _antProfessionRang.Rang;
        
        public ProfessionType TargetProfessionType { get; private set; }
        public ProfessionBase Profession { get; private set; }
        public int TargetProfessionRang { get; private set; }
        
        private AntProfessionRang _antProfessionRang;
        
        private void Start()
        {
            FrameworkCommander.GlobalData.UnitRepository.AddUnit(this);
            UnitPool.Instance.UnitCreation(this);
            
            resource.SetActive(false);
            SetProfession(defaultProfessionConfig);

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

            TargetMovePosition = transform.position;
            AutoGiveOrder(null, Transform.position);
        }

        private void Update()
        {
            Profession?.HandleUpdate(Time.deltaTime);
            
            foreach (var ability in _abilites)
                ability.OnUpdate(Time.deltaTime);
        }
        
        public override void GiveOrder(GameObject target, Vector3 position)
        {
            position.y = 0;
            AutoGiveOrder(target.GetComponent<IUnitTarget>(), position);
        }

        public void GiveOrderSwitchProfession(IUnitTarget unitTarget, AntProfessionConfigBase config) 
            => GiveOrderSwitchProfession(unitTarget, config.ProfessionType, config.AntProfessionRang);
        
        public void GiveOrderSwitchProfession(IUnitTarget unitTarget,
            ProfessionType newProfessionType, AntProfessionRang newProfessionRang)
        {
            if(newProfessionType == CurProfessionType && newProfessionRang.Rang == CurProfessionRang || 
               unitTarget.IsAnyNull())
            {
                Debug.LogWarning($"You try give order to set profession that already set ({newProfessionType == CurProfessionType}, {newProfessionRang.Rang == CurProfessionRang}) or Target is null ({unitTarget})");
                AutoGiveOrder(unitTarget);
                return;
            }

            TargetProfessionType = newProfessionType;
            TargetProfessionRang = newProfessionRang.Rang;
            
            HandleGiveOrder(unitTarget, UnitPathType.Switch_Profession);
        }

        public void SwitchProfession(AntProfessionConfigBase newProfession)
        {
            TrySetProfession(newProfession);
            AutoGiveOrder(null);
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
                    var workerLogic = new AntWorkerProfession(this, newProfession as AntWorkerConfig, resource);
                    Profession = workerLogic;
                    break;
                case (ProfessionType.MeleeWarrior):
                    Profession = new AntMeleeWarriorProfession(this, newProfession as AntMeleeWarriorConfig);
                    break;
                case (ProfessionType.RangeWarrior):
                    Profession  = new AntRangeWarriorProfession(this, newProfession as AntRangeWarriorConfig, _projectilesPool);
                    break;
                default: throw new NotImplementedException();
            }

            /*need because worker can have resource in the hands,
             so it is need for hide resource skin with profession changing*/
            resource.SetActive(false);
            _antProfessionRang = newProfession.AntProfessionRang;
            animator.runtimeAnimatorController = newProfession.AnimatorController;
        }
    }
}