using System;
using System.Collections.Generic;
using Constructions.LevelSystemCore;

namespace Constructions
{
    [Serializable]
    public class BeesWaxProduceLevelSystem : ConstructionLevelSystemBase<BeesWaxProduceLevel>
    {
        private ResourceConversionCore _resourceConversionCore;

        public BeesWaxProduceLevelSystem(IReadOnlyList<BeesWaxProduceLevel> levels, ref ResourceStorage healthStorage, 
            ref ResourceConversionCore resourceConversionCore)
            : base(levels, ref healthStorage)
        {
            _resourceConversionCore = resourceConversionCore =
                new ResourceConversionCore(CurrentLevel.ResourceConversionProccessInfo);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
        }
    }
}