using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.PoisonFog.Factory
{
    [CreateAssetMenu(fileName = nameof(PoisonFogFactoryConfig), menuName = "Configs/" + nameof(PoisonFogFactoryConfig))]
    public class PoisonFogFactoryConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public PoisonFogBehaviour PoisonFogPrefab { get; private set; }
    }
}