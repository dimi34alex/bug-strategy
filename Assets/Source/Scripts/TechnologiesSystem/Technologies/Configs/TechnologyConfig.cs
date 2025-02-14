using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    public class TechnologyConfig : ScriptableObject
    {
        [field: SerializeField] public TechnologyId Id { get; private set; }
    }
}