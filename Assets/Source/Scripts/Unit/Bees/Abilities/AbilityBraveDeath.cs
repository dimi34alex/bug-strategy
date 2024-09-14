using BugStrategy.CustomTimer;
using BugStrategy.Unit.AbilitiesCore;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    public sealed class AbilityBraveDeath : IPassiveAbility
    {
        private readonly Truten _truten;
        private readonly float _healValue;
        private readonly float _healRadius;
        private readonly LayerMask _healLayers;
        
        public IReadOnlyTimer Cooldown { get; } = new Timer(1, 1);
        public AbilityType AbilityType => AbilityType.BraveDeath;

        public AbilityBraveDeath(Truten truten, float healValue, float healRadius, LayerMask healLayers)
        {
            _truten = truten;
            _healValue = healValue;
            _healRadius = healRadius;
            _healLayers = healLayers;
            
            truten.OnDeactivation += HealNearAllies;
        }

        private void HealNearAllies(ITarget _)
        {
            RaycastHit[] result = new RaycastHit[100];
            var size = Physics.SphereCastNonAlloc(_truten.transform.position, _healRadius, Vector3.down,
                result, 0, _healLayers);

            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IHealable healable) && 
                    healable.Affiliation == _truten.Affiliation)
                {
                    healable.TakeHeal(_healValue);
                }
            }  
        }
    }
}