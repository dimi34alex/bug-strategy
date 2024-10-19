using System;
using BugStrategy.Trigger;
using BugStrategy.Unit.Factory;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Ants
{
    public class SwitchAntProfessionCunstruction : MonoBehaviour, ITarget, ITriggerable
    {
        //TODO: remove this script and create construction for switch professions
        [SerializeField] private AntBase ant;
        [SerializeField] private int targetProfessionRang;
        [SerializeField] private AntProfessionsConfigsRepository antProfessionsConfigsRepository;
        [SerializeField] private AffiliationEnum affiliationEnum;
    
        [Inject] private UnitFactory _unitFactory;
    
        public Transform Transform => transform;
        public TargetType TargetType => TargetType.Construction;
        public AffiliationEnum Affiliation => affiliationEnum;
        public bool IsActive { get; protected set; } = true;

        public event Action<ITriggerable> OnDisableITriggerableEvent;

        public event Action<ITarget> OnDeactivation;
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                GiveOrderSwitchProfession(ProfessionType.Worker);
            if (Input.GetKeyDown(KeyCode.S))
                GiveOrderSwitchProfession(ProfessionType.MeleeWarrior);
            if (Input.GetKeyDown(KeyCode.D))
                GiveOrderSwitchProfession(ProfessionType.RangeWarrior);
        
        
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _unitFactory.Create(UnitType.AntStandard, transform.position, affiliationEnum);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _unitFactory.Create(UnitType.AntBig, transform.position, affiliationEnum);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _unitFactory.Create(UnitType.AntFlying, transform.position, affiliationEnum);
        }

        private void GiveOrderSwitchProfession(ProfessionType newProfessionType)
            => ant.GiveOrderSwitchProfession(this, newProfessionType, targetProfessionRang);
        
        public bool TryTakeConfig(UnitType unitType, ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        {
            switch (unitType)
            {
                case UnitType.AntStandard:
                    return antProfessionsConfigsRepository.TryTakeStandardAntConfig(professionType, professionRang, out config);
                case UnitType.AntBig:
                    return antProfessionsConfigsRepository.TryTakeBigAntConfig(professionType, professionRang, out config);
                case UnitType.AntFlying:
                    return antProfessionsConfigsRepository.TryTakeFlyAntConfig(professionType, professionRang, out config);
                default:
                    config = null;
                    return false;
            }
        }

        private void OnDisable()
        {
            OnDisableITriggerableEvent?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnDeactivation?.Invoke(this);
        }
    }
}
