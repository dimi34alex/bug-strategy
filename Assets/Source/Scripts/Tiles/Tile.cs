using System;
using System.Linq;
using Avastrad.EventBusFramework;
using BugStrategy.CustomTimer;
using BugStrategy.Events;
using BugStrategy.Trigger;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class Tile : MonoBehaviour, ITriggerable
    {
        [SerializeField] private GameObject Model;
		[SerializeField] private GameObject WarFog;

		[Inject] private IEventBus _eventBus;
        
        private int _watchersCount;
        private Timer _visibleTimer;

        public bool IsVisible { get; private set; }

        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action<bool> OnVisibilityChanged;
        public event Action<Tile> OnDestroyed;

        private void Awake()
        {
            _visibleTimer = new Timer(5, 0, true);
            _visibleTimer.OnTimerEnd += MakeUnVisible;
        }

        private void OnDisable() 
            => OnDisableITriggerableEvent?.Invoke(this);
        
        private void OnDestroy() 
            => OnDestroyed?.Invoke(this);

        public void HandleUpdate(float deltaTime) 
            => _visibleTimer.Tick(deltaTime);
        
        public void AddWatcher()
        {
            _watchersCount++;

            if (_watchersCount == 1)
            {
                IsVisible = true;
                _visibleTimer.SetPause();
                OnVisibilityChanged?.Invoke(IsVisible);
                _eventBus.Invoke(new EventTileVisibilityChanged(IsVisible, transform.position));
            }
        }

        public void RemoveWatcher()
        {
            _watchersCount--;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if(_watchersCount < 0) 
                Debug.LogError("_watchersCount is " + _watchersCount);
#endif
            
            if (_watchersCount == 0) 
                _visibleTimer.Reset();
        }
        
        private void MakeUnVisible()
        {
            IsVisible = false;
            OnVisibilityChanged?.Invoke(IsVisible);
            _eventBus.Invoke(new EventTileVisibilityChanged(IsVisible, transform.position));
        }

        public void HideTileContent()
        {
			var ModelChildren = Model.GetComponentsInChildren<Transform>().Skip(1);
			var WarFogChildren = WarFog.GetComponentsInChildren<Transform>().Skip(1);

            if (ModelChildren.Count() == 0) return;

			foreach (var child in ModelChildren.Concat(WarFogChildren))
			{
				child.gameObject.SetActive(false);
			}
		}
    }
}
