using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    public abstract class AntProfessionConfigBase : ScriptableObject
    {
        public abstract ProfessionType ProfessionType { get; }

        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
    }
}