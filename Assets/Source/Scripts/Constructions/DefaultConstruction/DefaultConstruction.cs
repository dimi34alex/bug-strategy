using UnityEngine;

namespace BugStrategy.Constructions.DefaultConstruction
{
    public class DefaultConstruction : ConstructionBase
    {
        [SerializeField] private DefaultConstructionConfig config;
        
        public override FractionType Fraction => FractionType.None;
        public override ConstructionID ConstructionID => ConstructionID.TestConstruction;
        protected override ConstructionConfigBase ConfigBase => config;
    }
}
