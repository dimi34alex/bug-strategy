using System;
using Unit.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Unit
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
                    var unit = _unitFactory.Create(initUnitPair.UnitType);
                    unit.Transform.position = initUnitPair.Transform.position;
                    unit.SetAffiliation(initUnitsArray.Key);
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