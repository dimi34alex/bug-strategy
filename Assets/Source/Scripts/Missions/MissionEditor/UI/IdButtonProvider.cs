using System;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Missions.MissionEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class IdButtonProvider<TId> : MonoBehaviour
    {
        private TId _id;
        private Button _button;
        
        public event Action<TId> OnClick;

        public void Initialize(TId id)
        {
            _id = id;
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Clicked);
        }

        private void Clicked() 
            => OnClick?.Invoke(_id);
    }
}