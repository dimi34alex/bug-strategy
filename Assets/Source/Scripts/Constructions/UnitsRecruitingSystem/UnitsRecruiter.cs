using System;
using System.Collections.Generic;
using Unit.Factory;
using UnityEngine;

namespace UnitsRecruitingSystemCore
{
    public class UnitsRecruiter : IReadOnlyUnitsRecruiter
    {
        private readonly IAffiliation _affiliation;
        private readonly Transform _spawnTransform;
        private readonly UnitFactory _unitFactory;
        private readonly List<UnitRecruitingStack> _stacks;
        private readonly IResourceGlobalStorage _resourceGlobalStorage;
        private List<UnitRecruitingData> _recruitingDatas;

        public IReadOnlyList<UnitRecruitingData> UnitRecruitingData => _recruitingDatas;
        
        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public UnitsRecruiter(IAffiliation affiliation, int size, Transform spawnTransform, UnitFactory unitFactory, 
            IResourceGlobalStorage resourceGlobalStorage)
        {
            _affiliation = affiliation;
            _spawnTransform = spawnTransform;
            _unitFactory = unitFactory;
            _resourceGlobalStorage = resourceGlobalStorage;
            _stacks = new List<UnitRecruitingStack>();
            _recruitingDatas = new List<UnitRecruitingData>();

            for (int n = 0; n < size; n++)
            {
                var newStack = new UnitRecruitingStack();
                newStack.OnSpawnUnit += SpawnUnit;
                newStack.OnBecameEmpty += OnStackBecameEmpty;
                _stacks.Add(newStack);
            }
        }

        public void SetNewDatas(IReadOnlyList<UnitRecruitingData> newDatas)
        {
            _recruitingDatas = new List<UnitRecruitingData>(newDatas);
        }
        
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
            var recruitingData = _recruitingDatas.Find(data => data.CurrentID.Equals(id));
            return CheckCosts(recruitingData.Costs);
        }

        /// <summary>
        /// Check costs and resource count from ResourceGlobalStorage.
        /// </summary>
        /// <param name="costs"> costs </param>
        /// <returns> If player have enough resources then return true, else false </returns>
        public bool CheckCosts(IReadOnlyDictionary<ResourceID, int>  costs)
        {
            foreach (var cost in costs)
                if (cost.Value > _resourceGlobalStorage.GetResource(_affiliation.Affiliation, cost.Key).CurrentValue)
                    return false;
            
            return true;
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
            if (freeStackIndex == -1) throw new Exception("All stacks are busy");
            
            RecruitUnit(id, freeStackIndex);
        }

        /// <summary>
        /// Recruit unit if it possible, else throw exception 
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <param name="stackIndex"> index of empty stack </param>
        /// <exception cref="Exception"> Stack are busy </exception>
        /// <exception cref="Exception"> Need more resources </exception>
        public void RecruitUnit(UnitType id, int stackIndex)
        {
            if (!_stacks[stackIndex].Empty)
                throw new Exception("Stack are busy");

            var recruitingData = _recruitingDatas.Find(data => data.CurrentID.Equals(id));
            if (!CheckCosts(recruitingData.Costs))
                throw new Exception("Need more resources");
            
            foreach (var cost in recruitingData.Costs)
                _resourceGlobalStorage.ChangeValue(_affiliation.Affiliation, cost.Key, -cost.Value);

            _stacks[stackIndex].RecruitUnit(recruitingData);
            
            OnRecruitUnit?.Invoke();
            OnChange?.Invoke();
        }

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

        public void Tick(float time)
        {
            var allIsEmpty = true;
            foreach (var stack in _stacks)
                if (!stack.Empty)
                {
                    stack.Tick(time);
                    allIsEmpty = false;
                }

            if (!allIsEmpty)
                OnTick?.Invoke();
        }

        /// <returns>
        /// If Cancel is possible return true, else return false
        /// </returns>
        public bool CancelRecruit(int stackIndex)
        {
            var stack = _stacks[stackIndex];
            if (!stack.CancelRecruiting())
                return false;

            foreach (var cost in stack.CurrentData.Costs)
                _resourceGlobalStorage.ChangeValue(_affiliation.Affiliation, cost.Key, cost.Value);
            
            OnCancelRecruit?.Invoke();
            OnChange?.Invoke();

            return true;
        }

        public bool HaveFreeStack()
            => FindFreeStack() >= 0;

        /// <returns>
        /// Return list of information about all stacks
        /// </returns>
        public List<IReadOnlyUnitRecruitingStack> GetRecruitingInformation()
        {
            var fullInformation = new List<IReadOnlyUnitRecruitingStack>();
            foreach (var stack in _stacks)
                fullInformation.Add(stack);

            return fullInformation;
        }

        private void SpawnUnit(UnitType unitType)
        {
            float randomPosOffset = UnityEngine.Random.Range(-0.01f, 0.01f);
            var spawnPosition = _spawnTransform.position + Vector3.left * randomPosOffset;
            var unit = _unitFactory.Create(unitType, spawnPosition, _affiliation.Affiliation);
 
            unit.SetAffiliation(_affiliation.Affiliation);
        }

        private void OnStackBecameEmpty() 
            => OnChange?.Invoke();
    }
}