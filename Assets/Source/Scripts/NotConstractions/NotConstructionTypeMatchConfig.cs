using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.NotConstructions
{
    [CreateAssetMenu(fileName = "NotConstructionTypeMatchConfig", menuName = "Configs/NotConstructions/NotConstructionTypeMatchConfig")]
    public class NotConstructionTypeMatchConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private NotConstructionTypePair[] _notConstructionTypePairs;

        public IReadOnlyDictionary<NotConstructionType, IReadOnlyList<NotConstructionID>> NotConstructionTypePairs { get; private set; }
        public IReadOnlyDictionary<NotConstructionID, NotConstructionType> NotConstructionIDPairs { get; private set; }

        private void OnEnable()
        {
            NotConstructionTypePairs = _notConstructionTypePairs
                ?.ToDictionary(pair => pair.NotConstructionType, pair => (IReadOnlyList<NotConstructionID>)pair.NotConstructionsID);

            Dictionary<NotConstructionID, NotConstructionType> ids = new Dictionary<NotConstructionID, NotConstructionType>();

            if (_notConstructionTypePairs != null)
                foreach (NotConstructionTypePair pair in _notConstructionTypePairs)
                foreach (NotConstructionID notConstructionID in pair.NotConstructionsID)
                    ids.Add(notConstructionID, pair.NotConstructionType);

            NotConstructionIDPairs = ids;
        }

        public NotConstructionType GetNotConstructionType(NotConstructionID notConstructionID)
        {
            return NotConstructionIDPairs[notConstructionID];
        }


        [Serializable]
        public struct NotConstructionTypePair
        {
            public NotConstructionType NotConstructionType;
            public NotConstructionID[] NotConstructionsID;
        }
    }
}

