using System.Collections.Generic;
using UnityEngine;

public class DefaultConstruction : ConstructionBase
{
    public override FractionType Fraction => FractionType.None;
    public override ConstructionID ConstructionID => ConstructionID.TestConstruction;
}
