using System.Collections.Generic;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Missions
{
    [CreateAssetMenu(fileName = nameof(MissionConfig), menuName = "Configs/Missions/" + nameof(MissionConfig))]
    public class MissionConfig : ScriptableObject
    {
        [field: SerializeField] public TextAsset MissionJson { get; private set; }
        [field: SerializeField] public AffiliationEnum PlayerAffiliation { get; private set; }
        [SerializeField] private SerializableDictionary<AffiliationEnum, FractionType> fractionByAffiliation;
        [field: SerializeField] public float TimeBeforeAttackPlayerTownHall { get; private set; }
        
        public IReadOnlyDictionary<AffiliationEnum, FractionType> FractionByAffiliation => fractionByAffiliation;
    }
}