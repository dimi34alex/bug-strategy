using System;
using System.Collections.Generic;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Factory;
using BugStrategy.Unit.Pricing;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.RecruitingSystem
{

    public class UnitsRecruiter : IReadOnlyUnitsRecruiter
    {
        public IReadOnlyDictionary<UnitType, UnitRecruitingData> UnitRecruitingData { get; private set; }
        public IReadOnlyList<IReadOnlyUnitRecruitingStack> Stacks => _stacks;
        
        private AffiliationEnum Affiliation => _affiliation.Affiliation;
        
        private readonly IAffiliation _affiliation;
        private readonly Transform _spawnTransform;
        private readonly UnitFactory _unitFactory;
        private readonly List<UnitRecruitingStack> _stacks;
        private readonly ITeamsResourcesGlobalStorage _resourcesGlobalStorage;
        private readonly IUnitsCostsProvider _unitsCostsProvider;

        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public UnitsRecruiter(IAffiliation affiliation, int size, Transform spawnTransform, UnitFactory unitFactory, 
            ITeamsResourcesGlobalStorage resourcesGlobalStorage, IUnitsCostsProvider unitsCostsProvider)
        {
            _affiliation = affiliation;
            _spawnTransform = spawnTransform;
            _unitFactory = unitFactory;
            _resourcesGlobalStorage = resourcesGlobalStorage;
            _unitsCostsProvider = unitsCostsProvider;
            
            _stacks = new List<UnitRecruitingStack>();

            for (int n = 0; n < size; n++)
            {
                var newStack = new UnitRecruitingStack();
                newStack.OnSpawnUnit += SpawnUnit;
                newStack.OnBecameEmpty += OnStackBecameEmpty;
                _stacks.Add(newStack);
            }
        }
        
        public void Tick(float time)
        {
            var allIsEmpty = true;

            if (!_stacks[0].Empty)
            {
                _stacks[0].Tick(time);
                allIsEmpty = false;
            }

            if (!allIsEmpty)
                OnTick?.Invoke();
        }

        public IReadOnlyDictionary<ResourceID, int> GetUnitRecruitingCost(UnitType unitType) 
            => _unitsCostsProvider.GetUnitRecruitingCost(unitType);

        /// <returns> Returns first empty stack index. If it cant find free stack return -1 </returns>
        public int FindFreeStack()
        {
            for (int i = 0; i < _stacks.Count; i++)
                if (_stacks[i].Empty) return i;

            return -1;
        }

        /// <summary>
        /// Check unit costs and resource count from resourceRepository.
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <returns> If player have enough resources then return true, else false </returns>
        public bool CheckCosts(UnitType id)
        {
            var cost = _unitsCostsProvider.GetUnitRecruitingCost(id);
            return _resourcesGlobalStorage.CanBuy(Affiliation, cost);
        }

        /// <summary>
        /// Recruit unit if it possible, else throw exception 
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <exception cref="Exception"> All stacks are busy </exception>
        /// <exception cref="Exception"> Need more resources </exception>
        public void RecruitUnit(UnitType id)
        {
            int freeStackIndex = _stacks.IndexOf(freeStack => freeStack.Empty);
            if (freeStackIndex == -1) 
                throw new Exception("All stacks are busy");
            
            RecruitUnit(id, freeStackIndex);
        }

        /// <summary>
        /// Recruit unit if it possible, else throw exception 
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <param name="stackIndex"> index of empty stack </param>
        /// <exception cref="Exception"> Stack are busy </exception>
        /// <exception cref="Exception"> You try rec. unit without rec. data </exception>
        /// <exception cref="Exception"> Need more resources </exception>
        public void RecruitUnit(UnitType id, int stackIndex)
        {
            if (!_stacks[stackIndex].Empty)
                throw new Exception($"Stack are busy: [{id}] [{stackIndex}]");

            if (!UnitRecruitingData.ContainsKey(id))
                throw new Exception($"You try rec. unit without rec. data: [{id}] [{stackIndex}]");

            var recData = UnitRecruitingData[id];
            var cost = _unitsCostsProvider.GetUnitRecruitingCost(id);
            if (!_resourcesGlobalStorage.CanBuy(Affiliation, cost, recData.StackSize))
                throw new Exception($"Need more resources: [{id}] [{stackIndex}]");
            
            _resourcesGlobalStorage.ChangeValues(Affiliation, cost, -recData.StackSize);
            
            _stacks[stackIndex].RecruitUnit(id, UnitRecruitingData[id], cost);

            OnRecruitUnit?.Invoke();
            OnChange?.Invoke();
        }

        public void SetNewData(IReadOnlyDictionary<UnitType, UnitRecruitingData> newData) 
            => UnitRecruitingData = newData;

        /// <summary>
        /// Add new stacks if newCount upper then current size, else does nothing
        /// </summary>
        public void SetStackCount(int newCount)
        {
            if(newCount <= _stacks.Count) return;

            for (int n = _stacks.Count; n < newCount; n++)
            {
                var newStack = new UnitRecruitingStack();
                newStack.OnSpawnUnit += SpawnUnit;
                newStack.OnBecameEmpty += OnStackBecameEmpty;
                _stacks.Add(newStack);
            }

            OnAddStack?.Invoke();
            OnChange?.Invoke();
        }

        /// <returns>
        /// If cancel is success return true, else false
        /// </returns>
        public bool CancelRecruit(int stackIndex)
        {
            var stack = _stacks[stackIndex];
            var stackSize = stack.StackSize;
            var cost = stack.Costs;
            if (!stack.CancelRecruiting())
                return false;

            _resourcesGlobalStorage.ChangeValues(Affiliation, cost, stackSize);

            RecruitingQueue(stackIndex);

            OnCancelRecruit?.Invoke();
            OnChange?.Invoke();

            return true;
        }

        public bool HaveFreeStack()
            => FindFreeStack() >= 0;
        
        private void SpawnUnit(UnitType unitType)
        {
            float randomPosOffset = UnityEngine.Random.Range(-0.01f, 0.01f);
            var spawnPosition = _spawnTransform.position + Vector3.left * randomPosOffset;
            _unitFactory.Create(unitType, spawnPosition, Affiliation);

            if (_stacks[0].Empty) 
                RecruitingQueue(0);
        }

        private void RecruitingQueue(int stackIndex)
        {
            var newStack = _stacks[stackIndex];
            _stacks.RemoveAt(stackIndex);
            _stacks.Add(newStack);
        }

        private void OnStackBecameEmpty() 
            => OnChange?.Invoke();
    }
}