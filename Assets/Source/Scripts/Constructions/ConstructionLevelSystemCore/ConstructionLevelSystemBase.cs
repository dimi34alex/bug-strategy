using System.Collections.Generic;
using System;

namespace Constructions.LevelSystemCore
{
    public abstract class ConstructionLevelSystemBase<TConstructionLevel> : IConstructionLevelSystem
        where TConstructionLevel : ConstructionLevelBase
    {
        protected readonly IReadOnlyList<TConstructionLevel> Levels;
        protected readonly ResourceStorage HealthStorage;
        private readonly IResourceGlobalStorage _resourceGlobalStorage;
        private readonly ConstructionBase _construction;
        
        public int CurrentLevelIndex { get; private set; }
        public TConstructionLevel CurrentLevel => Levels[CurrentLevelIndex];
        public IReadOnlyDictionary<ResourceID, int> LevelUpCost => CurrentLevel.LevelUpCost;
        protected AffiliationEnum Affiliation => _construction.Affiliation;
        
        public event Action OnLevelUp;
        
        protected ConstructionLevelSystemBase(ConstructionBase construction, IReadOnlyList<TConstructionLevel> levels,
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage)
        {
            _construction = construction;
            Levels = levels;
            CurrentLevelIndex = 0;

            _resourceGlobalStorage = resourceGlobalStorage;
            HealthStorage = healthStorage;

            _construction.OnDestruction += OnConstructionDestruction;
        }

        public virtual void Init(int initialLevelIndex)
        {
            CurrentLevelIndex = initialLevelIndex;
            
            HealthStorage.SetCapacity(CurrentLevel.MaxHealPoints);
            HealthStorage.SetValue(CurrentLevel.MaxHealPoints);
            
            foreach (var resource in CurrentLevel.ResourceCapacity)
            {
                _resourceGlobalStorage.ChangeCapacity(Affiliation, resource.Key, resource.Value);

                if (resource.Key == ResourceID.Housing)
                    _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Housing, resource.Value);
            }
        }
        
        public bool LevelCapCheck() => CurrentLevelIndex < Levels.Count - 1;

        public bool LevelUpPriceCheck()
        {
            foreach (var resourceCost in CurrentLevel.LevelUpCost)
                if (_resourceGlobalStorage.GetResource(Affiliation, resourceCost.Key).CurrentValue < resourceCost.Value)
                    return false;

            return true;
        }
        
        public bool TryLevelUp()
        {
            if (!LevelCapCheck()) return false;
            if (!LevelUpPriceCheck()) return false;

            LevelUp();

            return true;
        }
        
        public void LevelUp()
        {
            LevelUpLogic();

            OnLevelUp?.Invoke();
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
                _resourceGlobalStorage.ChangeValue(Affiliation, resourceCost.Key, -resourceCost.Value);
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
                    _resourceGlobalStorage.ChangeCapacity(Affiliation, prevResource.Key,
                        CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value);

                    if (prevResource.Key == ResourceID.Housing)
                    {
                        _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Housing,
                            (CurrentLevel.ResourceCapacity[prevResource.Key] - prevResource.Value));
                    }
                }
                else
                {
                    _resourceGlobalStorage.ChangeCapacity(Affiliation, prevResource.Key, -prevResource.Value);
                }

                checkedResources.Add(prevResource.Key);
            }

            foreach (var resource in CurrentLevel.ResourceCapacity)
                if (!checkedResources.Contains(resource.Key))
                {
                    _resourceGlobalStorage.ChangeCapacity(Affiliation, resource.Key, resource.Value);

                    if (resource.Key == ResourceID.Housing)
                        _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Housing, resource.Value);
                }
        }

        protected virtual void OnConstructionDestruction()
        {
            RemoveResourcesCapacities();
        }

        /// <summary>
        /// Remove resources capacities for current level from resource global storage
        /// </summary>
        private void RemoveResourcesCapacities()
        {
            foreach (var resource in CurrentLevel.ResourceCapacity)
            {
                if (resource.Key == ResourceID.Housing)
                    _resourceGlobalStorage.ChangeValue(Affiliation, ResourceID.Housing, -resource.Value); 
                
                _resourceGlobalStorage.ChangeCapacity(Affiliation, resource.Key, -resource.Value);
            }
        }
    }
}