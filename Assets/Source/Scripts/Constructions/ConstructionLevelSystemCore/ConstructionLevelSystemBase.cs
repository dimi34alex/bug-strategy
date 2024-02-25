using System.Collections.Generic;
using UnityEngine;
using System;

namespace Constructions.LevelSystemCore
{
    [Serializable]
    public abstract class ConstructionLevelSystemBase<TConstructionLevel> : IConstructionLevelSystem
        where TConstructionLevel : ConstructionLevelBase
    {
        protected readonly IReadOnlyList<TConstructionLevel> Levels;

        public TConstructionLevel CurrentLevel => Levels[CurrentLevelIndex];
        public int CurrentLevelNum => CurrentLevelIndex + 1;

        protected int CurrentLevelIndex;
        protected ResourceStorage HealthStorage;

        public event Action OnLevelUp;

        protected ConstructionLevelSystemBase(IReadOnlyList<TConstructionLevel> levels, ref ResourceStorage healthStorage)
        {
            Levels = levels;
            CurrentLevelIndex = 0;

            HealthStorage = healthStorage = new ResourceStorage(CurrentLevel.MaxHealPoints, CurrentLevel.MaxHealPoints);

            foreach (var resource in CurrentLevel.ResourceCapacity)
            {
                ResourceGlobalStorage.ChangeCapacity(resource.Key, resource.Value);

                if (resource.Key == ResourceID.Housing)
                    ResourceGlobalStorage.ChangeValue(ResourceID.Housing, resource.Value);
            }
        }

        public bool LevelCapCheck() => CurrentLevelNum == Levels.Count;

        public bool PriceCheck()
        {
            foreach (var resource in CurrentLevel.LevelUpCost)
                if (!(ResourceGlobalStorage.GetResource(resource.Key).CurrentValue >= resource.Value))
                    return false;

            return true;
        }

        public bool TryLevelUp()
        {
            if (LevelCapCheck()) return false;
            if (!PriceCheck()) return false;

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
            foreach (var resource in CurrentLevel.LevelUpCost)
                ResourceGlobalStorage.ChangeValue(resource.Key, -resource.Value);
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
                    ResourceGlobalStorage.ChangeCapacity(prevResource.Key,
                        CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value);

                    if (prevResource.Key == ResourceID.Housing)
                    {
                        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,
                            (CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value));
                        Debug.Log(CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value);
                    }
                }
                else
                {
                    ResourceGlobalStorage.ChangeCapacity(prevResource.Key, -prevResource.Value);
                }

                checkedResources.Add(prevResource.Key);
            }

            foreach (var resource in CurrentLevel.ResourceCapacity)
                if (!checkedResources.Contains(resource.Key))
                {
                    ResourceGlobalStorage.ChangeCapacity(resource.Key, resource.Value);

                    if (resource.Key == ResourceID.Housing)
                    {
                        ResourceGlobalStorage.ChangeValue(ResourceID.Housing, resource.Value);
                        Debug.Log(resource.Value);
                    }
                }
        }
    }
}