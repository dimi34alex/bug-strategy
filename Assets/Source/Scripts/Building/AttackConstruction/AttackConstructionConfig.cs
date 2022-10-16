using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackConstructionConfig", menuName = "Config/AttackConstructionConfig")]
public class AttackConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private AttackConstructionPair[] _attackConstructionPairs;

    public IReadOnlyDictionary<ConstructionID, ConstructionConfiguration<AttackConstruction>> AttackConstructionPairs { get; private set; }

    private void OnEnable()
    {
        AttackConstructionPairs = _attackConstructionPairs?.ToDictionary(pair => pair.ConstructionID, pair => pair.Configuration);
    }

    public ConstructionConfiguration<AttackConstruction> GetConfiguration(ConstructionID constructionID)
    {
        if (!AttackConstructionPairs.ContainsKey(constructionID))
            throw new KeyNotFoundException($"Construction \"{constructionID}\" not found");

        return AttackConstructionPairs[constructionID];
    }


    [Serializable]
    public struct AttackConstructionPair
    {
        public ConstructionID ConstructionID;
        public ConstructionConfiguration<AttackConstruction> Configuration;
    }
}