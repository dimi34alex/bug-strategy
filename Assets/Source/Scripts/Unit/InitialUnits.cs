using System;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.Unit.Pricing;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit
{
    public class InitialUnits : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<AffiliationEnum, InitUnitPair[]> initUnits;

        [Inject] private readonly UnitFactory _unitFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _resourcesGlobalStorage;
        [Inject] private readonly IUnitsCostsProvider _unitsCostsProvider;
        
        private void Start()
        {
            foreach (var initUnitsArray in initUnits)
            {
                foreach (var initUnitPair in initUnitsArray.Value)
                {
                    var spawnPosition = initUnitPair.Transform.position;
                    _unitFactory.Create(initUnitPair.UnitType, spawnPosition, initUnitsArray.Key);
                    
                    var housingCount = _unitsCostsProvider.GetUnitRecruitingCost(initUnitPair.UnitType)[ResourceID.Housing];
                    _resourcesGlobalStorage.ChangeValue(initUnitsArray.Key, ResourceID.Housing, -housingCount);
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