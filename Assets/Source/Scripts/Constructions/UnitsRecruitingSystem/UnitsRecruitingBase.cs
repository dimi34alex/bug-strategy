using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    public abstract class UnitsRecruitingBase<TEnum> 
        where TEnum : Enum
    {
        private readonly Transform _spawnTransform;
        private readonly List<UnitRecruitingStack<TEnum>> _stacks;
        private List<UnitRecruitingData<TEnum>> _recruitingDatas;

        protected UnitsRecruitingBase(int size, Transform spawnTransform, List<UnitRecruitingData<TEnum>> newDatas)
        {
            _spawnTransform = spawnTransform;
            _stacks = new List<UnitRecruitingStack<TEnum>>();
            _recruitingDatas = newDatas;

            for (int n = 0; n < size; n++)
                _stacks.Add(new UnitRecruitingStack<TEnum>(spawnTransform));
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
        }
    
        public void SetNewDatas(List<UnitRecruitingData<TEnum>> newDatas)
        {
            _recruitingDatas = newDatas;
        }
    
        public void AddStacks(int newSize)
        {
            for (int n = _stacks.Count; n < newSize; n++)
                _stacks.Add(new UnitRecruitingStack<TEnum>(_spawnTransform));
        }

        public void Tick(float time)
        {
            foreach (var stack in _stacks)
                if (!stack.Empty)
                    stack.StackTick(time);
        }

        public List<IReadOnlyUnitRecruitingStack<TEnum>> GetRecruitingInformation()
        {
            var fullInformation = new List<IReadOnlyUnitRecruitingStack<TEnum>>();
            foreach (var stack in _stacks)
                fullInformation.Add(stack);
        
            return fullInformation;
        }

        public void CancelRecruiting(int stackIndex)
        {
            _stacks[stackIndex].CancelRecruiting();
        }
    } 
}