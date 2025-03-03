using System;
using System.Collections.Generic;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.Unit.Pricing;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class HousingBacker : IDisposable
    {
        private readonly UnitFactory _unitFactory;
        private readonly IUnitsCostsProvider _unitsCostsProvider;
        private readonly ITeamsResourcesGlobalStorage _resourcesGlobalStorage;
        private readonly HashSet<UnitBase> _registeredUnits = new(32);
        
        public HousingBacker(UnitFactory unitFactory, IUnitsCostsProvider unitsCostsProvider, ITeamsResourcesGlobalStorage resourcesGlobalStorage)
        {
            _unitFactory = unitFactory;
            _unitsCostsProvider = unitsCostsProvider;
            _resourcesGlobalStorage = resourcesGlobalStorage;

            _unitFactory.OnUnitCreated += Register;
        }

        private void Register(UnitBase unit)
        {
            if (_registeredUnits.Contains(unit))
            {
                Debug.LogWarning($"Duplicate of the unit: [{unit.UnitType}] [{unit.Affiliation}]");
                return;
            }
            
            _registeredUnits.Add(unit);
            unit.OnUnitDied += OnUnitDied;
        }

        private void OnUnitDied(UnitBase unit)
        {
            _registeredUnits.Remove(unit);
            unit.OnUnitDied -= OnUnitDied;

            if (unit.UnitType == UnitType.MobileHive)
                return;

            var housingCost = _unitsCostsProvider.GetUnitRecruitingCost(unit.UnitType)[ResourceID.Housing];
            _resourcesGlobalStorage.ChangeValue(unit.Affiliation, ResourceID.Housing, housingCost);
        }

        public void Dispose()
        {
            foreach (var registeredUnit in _registeredUnits) 
                registeredUnit.OnUnitDied -= OnUnitDied;

            _unitFactory.OnUnitCreated -= Register;
        }
    }
}