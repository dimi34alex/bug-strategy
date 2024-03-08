using UnityEngine;

namespace Constructions
{
    public class ButterflyPoisonFlowerPoisonFogProcessor
    {
        private readonly PoisonFog _poisonFogPrefab;
        private readonly Transform _flowerPosition;
        private PoisonFog _staticPoisonFog;
        private float _fogExistTime;
        private float _fogRadius;

        public ButterflyPoisonFlowerPoisonFogProcessor(Transform flowerPosition, PoisonFog poisonFogPrefab)
        {
            _flowerPosition = flowerPosition;
            _poisonFogPrefab = poisonFogPrefab;
        }

        public void SetData(float fogExistTime, float fogRadius, float staticFogRadius)
        {
            _fogExistTime = fogExistTime;
            _fogRadius = fogRadius;

            if (staticFogRadius > 0)
            {
                _staticPoisonFog ??= Object.Instantiate(_poisonFogPrefab, _flowerPosition.position, Quaternion.identity);
                _staticPoisonFog.Init(staticFogRadius, float.PositiveInfinity);
            }
            else
            {
                if(_staticPoisonFog != null)
                    Object.Destroy(_staticPoisonFog);
            }
        }
        
        public void SpawnPoisonFog()
        {
            _staticPoisonFog ??= Object.Instantiate(_poisonFogPrefab, _flowerPosition.position, Quaternion.identity);
            _staticPoisonFog.Init(_fogRadius, _fogExistTime);
        }
    }
}