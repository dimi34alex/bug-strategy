using System;
using System.Collections.Generic;

namespace UnitsRecruitingSystemCore
{
    public class UnitRecruitingStack : IReadOnlyUnitRecruitingStack
    {
        private readonly ResourceRepository _resourceRepository;
        
        private UnitRecruitingData _unitRecruitingData;
        
        public UnitType CurrentID => _unitRecruitingData.CurrentID;
        public float RecruitingTime => _unitRecruitingData.RecruitingTime;
        public int StackSize => _unitRecruitingData.StackSize;
        public float SpawnPauseTime => _unitRecruitingData.SpawnPauseTime;
        private IReadOnlyDictionary<ResourceID, int> Costs => _unitRecruitingData.Costs;
        
        public bool Empty { get; private set; }
        public float RecruitingTimer  { get; private set; }
        public float SpawnPauseTimer { get; private set; }
        public int SpawnedUnits { get; private set; }

        public event Action<UnitType> OnSpawnUnit; 
        
        public UnitRecruitingStack(ResourceRepository resourceRepository)
        {
            Empty = true;
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// Set new data if data is correct.
        /// </summary>
        /// <param name="newData"> new data </param>
        /// <exception cref="Exception"> Error: stack is not empty </exception>
        public void SetNewData(UnitRecruitingData newData)
        {
            if (!Empty) throw new Exception("Error: stack is not empty");

            Empty = false;
            
            _unitRecruitingData = newData;
            RecruitingTimer = 0;
            SpawnPauseTimer = newData.SpawnPauseTime;
            
            SpawnedUnits = 0;
        }

        public void StackTick(float time)
        {
            if (Empty) return;

            if (RecruitingTimer < RecruitingTime)
            {
                RecruitingTimer += time;
                return;
            }

            if (SpawnPauseTimer < SpawnPauseTime)
            {
                SpawnPauseTimer += time;
                return;
            }
            
            OnSpawnUnit?.Invoke(CurrentID);
                        
            SpawnedUnits++;
            SpawnPauseTimer = 0;

            if (SpawnedUnits >= StackSize)
                Empty = true;
        }

        /// <returns> If Cancel is possible return true, else return false </returns>
        public bool CancelRecruiting()
        {
            if (SpawnedUnits > 0) return false;

            foreach (var cost in Costs)
                _resourceRepository.ChangeValue(cost.Key, cost.Value);

            Empty = true;

            return true;
        }
    }
    
    public interface IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; }
        public UnitType CurrentID { get; }
        public float RecruitingTime { get; }
        public int StackSize { get; }
        public float SpawnPauseTime { get; }
        public float RecruitingTimer { get; }
        public float SpawnPauseTimer { get; }
        public int SpawnedUnits { get; }
    }
}