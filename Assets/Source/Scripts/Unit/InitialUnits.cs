using System;
using BugStrategy.Libs;
using BugStrategy.Unit.Factory;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit
{
    public class InitialUnits : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<AffiliationEnum, InitUnitPair[]> initUnits;

        [Inject] private readonly UnitFactory _unitFactory;
        
        private void Start()
        {
            foreach (var initUnitsArray in initUnits)
            {
                foreach (var initUnitPair in initUnitsArray.Value)
                {
                    var spawnPosition = initUnitPair.Transform.position;
                    _unitFactory.Create(initUnitPair.UnitType, spawnPosition, initUnitsArray.Key);
                }
            }
        }
        
        [Serializable]
        private struct InitUnitPair
        {
            [field: SerializeField] public UnitType UnitType { get; private set; }
            [field: SerializeField] public Transform Transform { get; private set; }
        }
    }
}