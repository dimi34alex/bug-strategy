using System.Collections.Generic;
using UnityEngine;
using System;

namespace ConstructionLevelSystem
{
    [Serializable]
    public class ConstructionLevelSystemBase<TConstructionLevel>
        where TConstructionLevel : ConstructionLevelBase
    {
        [SerializeField] protected List<TConstructionLevel> levels;

        public TConstructionLevel CurrentLevel => levels[currentLevelNum];
        public int CurrentLevelNum => currentLevelNum + 1;

        protected int currentLevelNum;
        protected ResourceStorage HealthStorage;

        public event Action OnLevelUp;

        protected ConstructionLevelSystemBase(ConstructionLevelSystemBase<TConstructionLevel> constructionLevelSystemBase, ref ResourceStorage healthStorage)
        {
            levels = constructionLevelSystemBase.levels;
            currentLevelNum = constructionLevelSystemBase.currentLevelNum;

            HealthStorage = healthStorage = new ResourceStorage(CurrentLevel.MaxHealPoints, CurrentLevel.MaxHealPoints);

            foreach (var resource in constructionLevelSystemBase.CurrentLevel.ResourceCapacity)
            {
                ResourceGlobalStorage.ChangeCapacity(resource.Key, resource.Value);

                if (resource.Key == ResourceID.Housing)
                    ResourceGlobalStorage.ChangeValue(ResourceID.Housing, resource.Value);
            }
        }

        public bool LevelCapCheck() => CurrentLevelNum == levels.Count;

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
            currentLevelNum++;

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