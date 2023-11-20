using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    public class UnitsRecruiter<TEnum> : IReadOnlyUnitsRecruiter<TEnum>
        where TEnum : Enum
    {
        private readonly Transform _spawnTransform;
        private readonly List<UnitRecruitingStack<TEnum>> _stacks;
        private List<UnitRecruitingData<TEnum>> _recruitingDatas;

        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public UnitsRecruiter(int size, Transform spawnTransform, IReadOnlyList<UnitRecruitingData<TEnum>> newDatas)
        {
            _spawnTransform = spawnTransform;
            _stacks = new List<UnitRecruitingStack<TEnum>>();
            _recruitingDatas = new List<UnitRecruitingData<TEnum>>(newDatas);

            for (int n = 0; n < size; n++)
                _stacks.Add(new UnitRecruitingStack<TEnum>(spawnTransform));
        }

        public void SetNewDatas(IReadOnlyList<UnitRecruitingData<TEnum>> newDatas)
        {
            _recruitingDatas = new List<UnitRecruitingData<TEnum>>(newDatas);
        }
        
        /// <returns> Returns first empty stack index. If it cant find free stack return -1 </returns>
        public int FindFreeStack()
        {
            for (int i = 0; i < _stacks.Count; i++)
                if (_stacks[i].Empty) return i;

            return -1;
        }

        /// <summary>
        /// Check unit costs and resource count from ResourceGlobalStorage.
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <returns> If player have enough resources then return true, else false </returns>
        public bool CheckCosts(TEnum id)
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
                if (cost.Value > ResourceGlobalStorage.GetResource(cost.Key).CurrentValue)
                    return false;
            
            return true;
        }
        
        /// <summary>
        /// Recruit unit if it possible, else throw exception 
        /// </summary>
        /// <param name="id"> unity id </param>
        /// <exception cref="Exception"> All stacks are busy </exception>
        /// <exception cref="Exception"> Need more resources </exception>
        public void RecruitUnit(TEnum id)
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
        public void RecruitUnit(TEnum id, int stackIndex)
        {
            if (!_stacks[stackIndex].Empty)
                throw new Exception("Stack are busy");

            var recruitingData = _recruitingDatas.Find(data => data.CurrentID.Equals(id));
            if (!CheckCosts(recruitingData.Costs)) throw new Exception("Need more resources");
            
            foreach (var cost in recruitingData.Costs)
                ResourceGlobalStorage.ChangeValue(cost.Key, -cost.Value);

            _stacks[stackIndex].SetNewData(recruitingData);
            
            OnRecruitUnit?.Invoke();
            OnChange?.Invoke();
        }

        /// <summary>
        /// Add new stacks if newCount upper then current size, else does nothing
        /// </summary>
        /// <param name="newCount"> new count of stacks </param>
        public void AddStacks(int newCount)
        {
            if(newCount <= _stacks.Count) return;
            
            for (int n = _stacks.Count; n <newCount; n++)
                _stacks.Add(new UnitRecruitingStack<TEnum>(_spawnTransform));

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

        /// <param name="stackIndex"> stack index </param>
        /// <returns> If Cancel is possible return true, else return false </returns>
        public bool CancelRecruit(int stackIndex)
        {
            if (!_stacks[stackIndex].CancelRecruiting())
                return false;

            OnCancelRecruit?.Invoke();
            OnChange?.Invoke();

            return true;
        }

        /// <returns> Return list of information about all stacks </returns>
        public List<IReadOnlyUnitRecruitingStack<TEnum>> GetRecruitingInformation()
        {
            var fullInformation = new List<IReadOnlyUnitRecruitingStack<TEnum>>();
            foreach (var stack in _stacks)
                fullInformation.Add(stack);

            return fullInformation;
        }
    }
}