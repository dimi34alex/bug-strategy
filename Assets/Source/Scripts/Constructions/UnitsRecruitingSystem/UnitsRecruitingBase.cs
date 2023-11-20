using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    public abstract class UnitsRecruiting<TEnum> : IReadOnlyUnitsRecruiting<TEnum>
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
        
        protected UnitsRecruiting(int size, Transform spawnTransform, List<UnitRecruitingData<TEnum>> newDatas)
        {
            _spawnTransform = spawnTransform;
            _stacks = new List<UnitRecruitingStack<TEnum>>();
            _recruitingDatas = newDatas;

            for (int n = 0; n < size; n++)
                _stacks.Add(new UnitRecruitingStack<TEnum>(spawnTransform));
        }
    
        public void SetNewDatas(List<UnitRecruitingData<TEnum>> newDatas)
        {
            _recruitingDatas = newDatas;
        }
        
        public void RecruitUnit(TEnum id, out string errorLog)
        {
            errorLog = "";

            int foundStack = _stacks.IndexOf(freeStack => freeStack.Empty);
            if (foundStack == -1)
            {
                errorLog = "All stacks are busy";
                Debug.LogWarning(errorLog);
                return;
            }

            var foundData = _recruitingDatas.Find(found => found.CurrentID.Equals(id));
        
            foreach (var cost in foundData.Costs)
            {
                if (cost.Value > ResourceGlobalStorage.GetResource(cost.Key).CurrentValue)
                {
                    errorLog = "Need more resources";
                    Debug.LogWarning(errorLog);
                    return;
                } 
            }
        
            foreach (var cost in foundData.Costs)
                ResourceGlobalStorage.ChangeValue(cost.Key, -cost.Value);
        
            _stacks[foundStack].SetNewData(foundData);
            
            OnRecruitUnit?.Invoke();
            OnChange?.Invoke();
        }
        
        public void AddStacks(int newSize)
        {
            for (int n = _stacks.Count; n < newSize; n++)
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
        
        public void CancelRecruit(int stackIndex)
        {
            _stacks[stackIndex].CancelRecruiting();
            
            OnCancelRecruit?.Invoke();
            OnChange?.Invoke();
        }
        
        public List<IReadOnlyUnitRecruitingStack<TEnum>> GetRecruitingInformation()
        {
            var fullInformation = new List<IReadOnlyUnitRecruitingStack<TEnum>>();
            foreach (var stack in _stacks)
                fullInformation.Add(stack);
        
            return fullInformation;
        }
    }
}