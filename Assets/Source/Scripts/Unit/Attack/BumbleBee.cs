using System;
using Unit.Ants.ProfessionsConfigs;
using Unit.Professions;
using UnityEngine;

public class BumbleBee : BeeUnit
{
    public override IReadOnlyProfession CurrentProfession => throw new NullReferenceException();
    
    public override UnitType UnitType => UnitType.Bumblebee;
}
    