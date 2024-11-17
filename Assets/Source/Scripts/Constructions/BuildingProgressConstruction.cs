using System;
using System.Collections;
using CycleFramework.Extensions;
using TMPro;
using UnityEngine;

namespace BugStrategy.Constructions
{
    public class BuildingProgressConstruction : ConstructionBase
    {
        [SerializeField] private TMP_Text _timerText;

        public override FractionType Fraction => FractionType.None;
        public override ConstructionID ConstructionID => ConstructionID.BuildingProgressConstruction;
        public ConstructionID BuildingConstructionID { get; private set; }

        public BuildingProgressState BuildingProgressState { get; private set; } = BuildingProgressState.Waiting;

        public event Action<BuildingProgressConstruction> OnTimerEnd;

        private GameObject currentWorker;
        public bool WorkerArrived;
    
        public void StartBuilding(float duration, ConstructionID constructionID)
        {
            if (BuildingProgressState != BuildingProgressState.Waiting)
                return;
            BuildingProgressState = BuildingProgressState.Started;
            BuildingConstructionID = constructionID;
            StartCoroutine(StartBuildingCoroutine(duration));
        }

        private IEnumerator StartBuildingCoroutine(float duration)
        {
            var timer = duration;

            while (timer > 0)
            {
                _timerText.text = $"{timer.SecsToMins()}";
                yield return new WaitForSeconds(1f);
                if (WorkerArrived)
                {
                    timer--;
                }
            }

            _timerText.text = $"{timer.SecsToMins()}";

            yield return new WaitForSeconds(1f);
            BuildingProgressState = BuildingProgressState.Completed;

            OnTimerEnd?.Invoke(this);
        }
    }
}
