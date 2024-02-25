using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystemCore
{
    public class UnitsRecruiter : IReadOnlyUnitsRecruiter
    {
        private readonly Transform _spawnTransform;
        private readonly List<UnitRecruitingStack<UnitType>> _stacks;
        private List<UnitRecruitingData<UnitType>> _recruitingDatas;
        private ResourceRepository _resourceRepository;
        
        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public UnitsRecruiter(int size, Transform spawnTransform, IReadOnlyList<UnitRecruitingData<UnitType>> newDatas,
            ref ResourceRepository resourceRepository)
        {
            _spawnTransform = spawnTransform;
            _resourceRepository = resourceRepository;
            _stacks = new List<UnitRecruitingStack<UnitType>>();
            _recruitingDatas = new List<UnitRecruitingData<UnitType>>(newDatas);

            for (int n = 0; n < size; n++)
                _stacks.Add(new UnitRecruitingStack<UnitType>(spawnTransform, ref resourceRepository));
        }

        public void SetNewDatas(IReadOnlyList<UnitRecruitingData<UnitType>> newDatas)
        {
            _recruitingDatas = new List<UnitRecruitingData<UnitType>>(newDatas);
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
                if (cost.Value > _resourceRepository.GetResource(cost.Key).CurrentValue)
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
                _resourceRepository.ChangeValue(cost.Key, -cost.Value);

            _stacks[stackIndex].SetNewData(recruitingData);
            
            OnRecruitUnit?.Invoke();
            OnChange?.Invoke();
        }

        /// <summary>
        /// Add new stacks if newCount upper then current size, else does nothing
        /// </summary>
        public void AddStacks(int newCount)
        {
            if(newCount <= _stacks.Count) return;
            
            for (int n = _stacks.Count; n <newCount; n++)
                _stacks.Add(new UnitRecruitingStack<UnitType>(_spawnTransform, ref _resourceRepository));

            OnAddStack?.Invoke();
            OnChange?.Invoke();
        }

        public void Tick(float time)
        {
            foreach (var stack in _stacks)
                if (!stack.Empty)
                    stack.StackTick(time);

            OnTick?.Invoke();
            OnChange?.Invoke();
        }

        /// <returns>
        /// If Cancel is possible return true, else return false
        /// </returns>
        public bool CancelRecruit(int stackIndex)
        {
            if (!_stacks[stackIndex].CancelRecruiting())
                return false;

            OnCancelRecruit?.Invoke();
            OnChange?.Invoke();

            return true;
        }

        /// <returns>
        /// Return list of information about all stacks
        /// </returns>
        public List<IReadOnlyUnitRecruitingStack<UnitType>> GetRecruitingInformation()
        {
            var fullInformation = new List<IReadOnlyUnitRecruitingStack<UnitType>>();
            foreach (var stack in _stacks)
                fullInformation.Add(stack);

            return fullInformation;
        }
    }
}