using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs
{
    [Serializable]
    public class RecruitingEvaluationConfig
    {
        [SerializeField] private AnimationCurve unitsLimitCurve;
        [SerializeField] private float priorityThreshHold = 0.5f;
        [SerializeField] private SerializableDictionary<ResourceID, AnimationCurve> resourceLimitsCurves;
        
        public AnimationCurve UnitsLimitCurve => unitsLimitCurve;
        public float PriorityThreshHold => priorityThreshHold;
        public IReadOnlyDictionary<ResourceID, AnimationCurve> ResourceLimitsCurves => resourceLimitsCurves;
    }
}