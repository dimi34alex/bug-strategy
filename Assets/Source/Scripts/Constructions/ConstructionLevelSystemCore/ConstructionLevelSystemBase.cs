using System.Collections.Generic;
using UnityEngine;
using System;

namespace Constructions.LevelSystemCore
{
    public abstract class ConstructionLevelSystemBase<TConstructionLevel> : IConstructionLevelSystem
        where TConstructionLevel : ConstructionLevelBase
    {
        protected readonly IReadOnlyList<TConstructionLevel> Levels;
        protected readonly ResourceStorage HealthStorage;

        public TConstructionLevel CurrentLevel => Levels[CurrentLevelIndex];
        public IReadOnlyDictionary<ResourceID, int> LevelUpCost => CurrentLevel.LevelUpCost;
        public int CurrentLevelIndex { get; private set; }
        
        public event Action OnLevelUp;

        private readonly ResourceRepository _resourceRepository;
        
        protected ConstructionLevelSystemBase(IReadOnlyList<TConstructionLevel> levels, 
            ref ResourceRepository resourceRepository, ref ResourceStorage healthStorage)
        {
            Levels = levels;
            CurrentLevelIndex = 0;

            _resourceRepository = resourceRepository;
            HealthStorage = healthStorage = new ResourceStorage(CurrentLevel.MaxHealPoints, CurrentLevel.MaxHealPoints);

            foreach (var resource in CurrentLevel.ResourceCapacity)
            {
                _resourceRepository.ChangeCapacity(resource.Key, resource.Value);

                if (resource.Key == ResourceID.Housing)
                    _resourceRepository.ChangeValue(ResourceID.Housing, resource.Value);
            }
        }

        public bool LevelCapCheck() => CurrentLevelIndex < Levels.Count - 1;

        public bool LevelUpPriceCheck()
        {
            foreach (var resourceCost in CurrentLevel.LevelUpCost)
                if (_resourceRepository.GetResource(resourceCost.Key).CurrentValue < resourceCost.Value)
                    return false;

            return true;
        }
        
        public bool TryLevelUp()
        {
            if (!LevelCapCheck()) return false;
            if (!LevelUpPriceCheck()) return false;

            LevelUpLogic();

            OnLevelUp?.Invoke();

            return true;
        }

        protected virtual void LevelUpLogic()
        {
            SpendResources();

            IReadOnlyDictionary<ResourceID, int> prevResourceCapacity = CurrentLevel.ResourceCapacity;
            CurrentLevelIndex++;

            ReCalculateResourceCapacity(prevResourceCapacity);
            ReCalculateHealthPoints();
        }

        protected void SpendResources()
        {
            foreach (var resourceCost in CurrentLevel.LevelUpCost)
                _resourceRepository.ChangeValue(resourceCost.Key, -resourceCost.Value);
        }

        protected void ReCalculateHealthPoints()
        {
            if (HealthStorage.CurrentValue >= HealthStorage.Capacity)
            {
                HealthStorage.SetCapacity(CurrentLevel.MaxHealPoints);
                HealthStorage.SetValue(CurrentLevel.MaxHealPoints);
            }
            else
            {
                HealthStorage.SetCapacity(CurrentLevel.MaxHealPoints);
            }
        }

        protected void ReCalculateResourceCapacity(IReadOnlyDictionary<ResourceID, int> prevResourceCapacity)
        {
            List<ResourceID> checkedResources = new List<ResourceID>();
            foreach (var prevResource in prevResourceCapacity)
            {
                if (CurrentLevel.ResourceCapacity.ContainsKey(prevResource.Key))
                {
                    _resourceRepository.ChangeCapacity(prevResource.Key,
                        CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value);

                    if (prevResource.Key == ResourceID.Housing)
                    {
                        _resourceRepository.ChangeValue(ResourceID.Housing,
                            (CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value));
                    }
                }
                else
                {
                    _resourceRepository.ChangeCapacity(prevResource.Key, -prevResource.Value);
                }

                checkedResources.Add(prevResource.Key);
            }

            foreach (var resource in CurrentLevel.ResourceCapacity)
                if (!checkedResources.Contains(resource.Key))
                {
                    _resourceRepository.ChangeCapacity(resource.Key, resource.Value);

                    if (resource.Key == ResourceID.Housing)
                    {
                        _resourceRepository.ChangeValue(ResourceID.Housing, resource.Value);
                        Debug.Log(resource.Value);
                    }
                }
        }
    }
}