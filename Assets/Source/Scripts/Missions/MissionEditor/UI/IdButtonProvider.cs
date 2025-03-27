using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Missions.MissionEditor.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class IdButtonProvider<TId> : MonoBehaviour
    {
        [SerializeField] protected TMP_Text tmpText;
        
        private TId _id;
        private Button _button;
        
        public event Action<TId> OnClick;

        public void Initialize(TId id)
        {
            _id = id;

            tmpText.text = GetName(_id);
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Clicked);
        }

        protected virtual string GetName(TId id) 
            => id.ToString();

        private void Clicked() 
            => OnClick?.Invoke(_id);
    }
}