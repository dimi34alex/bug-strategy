using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class BeesWaxProduceLevelSystem : ConstructionLevelSystemBase<BeesWaxProduceLevel>
    {
        private ResourceConversionCore _resourceConversionCore;

        public BeesWaxProduceLevelSystem(ConstructionLevelSystemBase<BeesWaxProduceLevel> constructionLevelSystemBase,
            ref ResourceStorage healthStorage, ref ResourceConversionCore resourceConversionCore)
            : base(constructionLevelSystemBase, ref healthStorage)
        {
            _resourceConversionCore = resourceConversionCore =
                new ResourceConversionCore(constructionLevelSystemBase.CurrentLevel.ResourceConversionProccessInfo);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _resourceConversionCore.SetResourceProduceProccessInfo(CurrentLevel.ResourceConversionProccessInfo);
        }
    }
}