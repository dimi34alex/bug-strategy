using UnityEngine;

namespace Constructions
{
    public class BeeStickyTile : ConstructionBase
    {
        [SerializeField] private BeeStickyTileConfig config;
        [SerializeField] private TriggerBehaviour _triggerBehaviour;
        
        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.Bee_Sticky_Tile_Construction;

        protected override void OnAwake()
        {
            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
        }

        protected override void OnStart()
        {
            _triggerBehaviour.EnterEvent += OnUnitEnter;
            _triggerBehaviour.ExitEvent += OnUnitExit;
        }

        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out MovingUnit movingUnit))
            {
                movingUnit.ChangeContainsStickyTiles(1);
            }
        }

        private void OnUnitExit(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out MovingUnit movingUnit))
            {
                movingUnit.ChangeContainsStickyTiles(-1);
            }
        }
    }
}