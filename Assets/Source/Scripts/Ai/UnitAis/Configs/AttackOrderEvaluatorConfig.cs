using System;
using UnityEngine;

namespace Source.Scripts.Ai.UnitAis.Configs
{
    [Serializable]
    public class AttackOrderEvaluatorConfig
    {
        [Header("Auto state:")]
        [SerializeField] private AnimationCurve animationCurve;
        
        [Space]
        [Header("Attack state:")]
        [SerializeField] private AnimationCurve animationCurve2;
    }
}