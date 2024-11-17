using BugStrategy.Pool;
using UnityEngine;
using Zenject;

namespace BugStrategy.PoisonFog.Factory
{
    public class PoisonFogFactory : MonoBehaviour
    {
        [Inject] private PoisonFogFactoryConfig _poisonFogFactoryConfig;
        [Inject] private PoisonFogsRepository _poisonFogsRepository;
        
        private Pool<PoisonFogBehaviour> _pool;

        private void Awake()
        {
            _pool = new Pool<PoisonFogBehaviour>(InstantiatePoisonFog);
        }

        public PoisonFogBehaviour Create()
            => Create(Vector3.zero);

        public PoisonFogBehaviour Create(Vector3 position)
        {
            var fog = _pool.ExtractElement();
            fog.transform.position = position;
            _poisonFogsRepository.Add(fog);
            
            return fog;
        }

        private PoisonFogBehaviour InstantiatePoisonFog()
            => Instantiate(_poisonFogFactoryConfig.PoisonFogPrefab, transform);
    }
}