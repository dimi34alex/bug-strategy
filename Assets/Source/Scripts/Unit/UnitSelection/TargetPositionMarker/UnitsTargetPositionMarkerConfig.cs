using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Unit.UnitSelection.TargetPositionMarker
{
    [CreateAssetMenu(fileName = nameof(UnitsTargetPositionMarkerConfig), menuName = "Configs/" + nameof(UnitsTargetPositionMarkerConfig))]
    public class UnitsTargetPositionMarkerConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public UnitsTargetPositionMarker Prefab { get; private set; }
        [field: SerializeField] public int MaxMarkersCountPerTime { get; private set; } = 3;
    }
}