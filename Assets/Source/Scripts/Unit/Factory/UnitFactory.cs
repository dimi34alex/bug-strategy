using System;
using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.Pool;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit.Factory
{
    public class UnitFactory : MonoBehaviour
    {
        [Inject] private readonly UnitsPrefabsConfig _prefabsConfig;
        [Inject] private readonly UnitRepository _unitRepository;
        [Inject] private readonly DiContainer _container;

        private Pool<UnitBase, UnitType> _pool;
        private readonly Dictionary<UnitType, Transform> _parents = new();

        public event Action<UnitBase> OnUnitCreated; 
        
        private void Awake()
        {
            _pool = new Pool<UnitBase, UnitType>(InstantiateUnit);
            
            var types = EnumValuesTool.GetValues<UnitType>();
            foreach (var type in types)
            {
                var parent = new GameObject()
                {
                    name = type.ToString(),
                    transform = { parent = this.gameObject.transform }
                };
                _parents.Add(type, parent.transform);
            }
        }

        public UnitBase Create(UnitType unitType, Vector3 position, AffiliationEnum affiliation)
        {
            var unit = _pool.ExtractElement(unitType);
            unit.Transform.position = position;
            unit.Initialize(affiliation);
            _unitRepository.AddUnit(unit);
            OnUnitCreated?.Invoke(unit);
            
            return unit;
        }

        private UnitBase InstantiateUnit(UnitType unitType)
        {
            if (!_prefabsConfig.Data.ContainsKey(unitType))
                throw new IndexOutOfRangeException($"Type {unitType} dont present in {_prefabsConfig}");
                    
            var unit = _container.InstantiatePrefab(_prefabsConfig.Data[unitType], _parents[unitType]);
            if (!unit.TryGetComponent<UnitBase>(out var unitBase))
                throw new NullReferenceException($"Prefab with Key {unitType}" +
                                                 $" dont have script {nameof(UnitBase)}");
            
            return unitBase;
        }
    }
}