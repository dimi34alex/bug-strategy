using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Missions
{
    [CreateAssetMenu(fileName = nameof(MissionsConfig), menuName = "Configs/Missions/" + nameof(MissionsConfig))]
    public class MissionsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<MissionConfig> missionsConfigs;

        public IReadOnlyList<MissionConfig> MissionsConfigs => missionsConfigs;
    }
}