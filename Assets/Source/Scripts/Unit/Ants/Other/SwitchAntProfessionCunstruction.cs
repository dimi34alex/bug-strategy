using System;
using Unit.Ants;
using Unit.Ants.ProfessionsConfigs;
using Unit.Professions;
using UnityEngine;

public class SwitchAntProfessionCunstruction : MonoBehaviour, IUnitTarget, ITriggerable
{
    //TODO: remove this script and create construction for switch professions
    [SerializeField] private AntBase ant;
    [SerializeField] private AntProfessionRang targetProfessionRang;
    [SerializeField] private AntProfessionsConfigsRepository antProfessionsConfigsRepository;
    
    public Transform Transform => transform;
    public UnitTargetType TargetType => UnitTargetType.Construction;
    public AffiliationEnum Affiliation => AffiliationEnum.Ants;
    
    public event Action<ITriggerable> OnDisableITriggerableEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GiveOrderSwitchProfession(ProfessionType.Worker);
        if (Input.GetKeyDown(KeyCode.S))
            GiveOrderSwitchProfession(ProfessionType.MeleeWarrior);
        if (Input.GetKeyDown(KeyCode.D))
            GiveOrderSwitchProfession(ProfessionType.RangeWarrior);
    }

    private void GiveOrderSwitchProfession(ProfessionType newProfessionType) 
        => ant.GiveOrderSwitchProfession(this, newProfessionType, targetProfessionRang);

    public bool TryTakeConfig(ProfessionType professionType, int professionRang, out AntProfessionConfigBase config)
        => antProfessionsConfigsRepository.TryTakeConfig(professionType, professionRang, out config);
}
