using Source.Scripts.ResourcesSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.EntityInfo
{
    public abstract class EntityInfoScreen : UIScreen
    {
        [SerializeField] private Image _infoImage;
        private HealthView _healthView;

        private IReadOnlyFloatStorage _health;

        private void Awake()
        {
            OnAwake();
        }

        protected void OnAwake()
        {
            _healthView = gameObject.GetComponentInChildren<HealthView>(true);
        }

        public void SetHealthPointsInfo(Sprite sprite, IReadOnlyFloatStorage storage)
        {
            _healthView.Init(storage);

            _health = storage;

            _infoImage.sprite = sprite;
        }

        public virtual void Show() 
            => gameObject.SetActive(true);

        public virtual void Hide() 
            => gameObject.SetActive(false);
    }
}