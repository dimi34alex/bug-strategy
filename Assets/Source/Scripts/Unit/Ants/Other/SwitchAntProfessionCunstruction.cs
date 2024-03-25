using System;
using Unit.Ants;
using Unit.Ants.Configs.Professions;
using Unit.Ants.Professions;
using Unit.Factory;
using Unit.OrderValidatorCore;
using UnityEngine;
using Zenject;

public class SwitchAntProfessionCunstruction : MonoBehaviour, IUnitTarget, ITriggerable
{
    //TODO: remove this script and create construction for switch professions
    [SerializeField] private AntBase ant;
    [SerializeField] private AntProfessionRang targetProfessionRang;
    [SerializeField] private AntProfessionsConfigsRepository antProfessionsConfigsRepository;

    [Inject] private UnitFactory _unitFactory;
    
    public Transform Transform => transform;
    public UnitTargetType TargetType => UnitTargetType.Construction;
    public AffiliationEnum Affiliation => AffiliationEnum.Ants;
    public bool IsActive { get; protected set; } = true;

    public event Action<ITriggerable> OnDisableITriggerableEvent;

    public event Action OnDeactivation;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GiveOrderSwitchProfession(ProfessionType.Worker);
        if (Input.GetKeyDown(KeyCode.S))
            GiveOrderSwitchProfession(ProfessionType.MeleeWarrior);
        if (Input.GetKeyDown(KeyCode.D))
            GiveOrderSwitchProfession(ProfessionType.RangeWarrior);
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _unitFactory.Create(UnitType.AntStandard).transform.position = transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _unitFactory.Create(UnitType.AntBig).transform.position = transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _unitFactory.Create(UnitType.AntFlying).transform.position = transform.position;
    }

    private void GiveOrderSwitchProfession(ProfessionType newProfessionType) 
        => ant.GiveOrderSwitchProfession(this, newProfessionType, targetProfessionRang);

    public bool TryTakeConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        => antProfessionsConfigsRepository.TryTakeConfig(professionType, professionRang, out config);
}
