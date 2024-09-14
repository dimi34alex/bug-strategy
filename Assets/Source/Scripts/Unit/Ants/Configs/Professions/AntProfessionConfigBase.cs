using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    public abstract class AntProfessionConfigBase : ScriptableObject
    {
        public abstract ProfessionType ProfessionType { get; }

        [field: SerializeField] public AntProfessionRang AntProfessionRang { get; private set; }
        [field: SerializeField] public AnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] private List<UnitType> access { get; set; }

        public int ProfessionRang => AntProfessionRang.Rang;
        public IReadOnlyList<UnitType> Access => access;
    }
}