using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Constructions
{
    public class BeeLandmine : ConstructionBase, IDamageApplicator
    {
        [SerializeField] private BeeLandmineConfig config;
        [SerializeField] private TriggerBehaviour _triggerBehaviour;
        
        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeLandmine;

        public float Damage { get; private set; }
        
        protected override void OnAwake()
        {
            _healthStorage = new ResourceStorage(config.HealthPoints, config.HealthPoints);
            Damage = config.Damage;
        }

        protected override void OnStart()
        {
            _triggerBehaviour.EnterEvent += OnUnitEnter;
        }

        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                if (unit.Affiliation == AffiliationEnum.Ants ||
                    unit.Affiliation == AffiliationEnum.Butterflies)
                {
                    _triggerBehaviour.EnterEvent -= OnUnitEnter;
                    DOTween.Sequence()
                        .AppendInterval(config.ResponseDelay)
                        .AppendCallback(Explosion);
                }
            }
        }

        private void Explosion()
        {
            ITriggerable[] elements = _triggerBehaviour.ContainsComponents.ToArray();
            foreach (var element in elements)
                if(element.TryCast(out UnitBase unit))
                    unit.TakeDamage(this);
            
            Destroy(gameObject);
        }
    }
}