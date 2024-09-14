using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions
{
    [CreateAssetMenu(fileName = "ConstructionTypeMatchConfig", menuName = "Config/ConstructionTypeMatchConfig")]
    public class ConstructionTypeMatchConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionTypePair[] _constructionTypePairs;

        public IReadOnlyDictionary<ConstructionType, IReadOnlyList<ConstructionID>> ConstructionTypePairs { get; private set; }
        public IReadOnlyDictionary<ConstructionID, ConstructionType> ConstructionIDPairs { get; private set; }

        private void OnEnable()
        {
            ConstructionTypePairs = _constructionTypePairs
                ?.ToDictionary(pair => pair.ConstructionType, pair => (IReadOnlyList<ConstructionID>)pair.ConstructionsID);

            Dictionary<ConstructionID, ConstructionType> ids = new Dictionary<ConstructionID, ConstructionType>();

            if (_constructionTypePairs != null)
                foreach (ConstructionTypePair pair in _constructionTypePairs)
                foreach (ConstructionID constructionID in pair.ConstructionsID)
                    ids.Add(constructionID, pair.ConstructionType);

            ConstructionIDPairs = ids;
        }

        public ConstructionType GetConstructionType(ConstructionID constructionID)
        {
            return ConstructionIDPairs[constructionID];
        }


        [Serializable]
        public struct ConstructionTypePair
        {
            public ConstructionType ConstructionType;
            public ConstructionID[] ConstructionsID;
        }
    }
}

