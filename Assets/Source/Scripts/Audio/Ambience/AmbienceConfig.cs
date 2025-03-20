using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Audio.Ambience
{
    [CreateAssetMenu(fileName = nameof(AmbienceConfig), menuName = "Configs/Audio/" + nameof(AmbienceConfig))]
    public class AmbienceConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float TransitionTime { get; private set; } = 1f;
    }
}