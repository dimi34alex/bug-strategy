using UnityEngine;

namespace Poison
{
    [CreateAssetMenu(fileName = nameof(PoisonFogFactoryConfig), menuName = "Cpnfigs/" + nameof(PoisonFogFactoryConfig))]
    public class PoisonFogFactoryConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public PoisonFog PoisonFogPrefab { get; private set; }
    }
}