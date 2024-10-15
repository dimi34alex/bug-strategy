using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.MissionEditor.UI
{
    [Serializable]
    public class ButtonsInitializer<TId>
    {
        [SerializeField] private Transform parent;
        [SerializeField] private IdButtonProvider<TId> buttonPrefab;
        
        public event Action<TId> OnPressed;

        public void Initialize(IEnumerable<TId> keys)
        {
            foreach (var key in keys)
            {
                var b = Object.Instantiate(buttonPrefab, parent);
                b.Initialize(key);
                b.OnClick += SendEvent;
            }
        }

        private void SendEvent(TId id) => OnPressed?.Invoke(id);
    }
}