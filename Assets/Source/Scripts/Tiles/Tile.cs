using System;
using BugStrategy.CustomTimer;
using BugStrategy.Trigger;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class Tile : MonoBehaviour, ITriggerable
    {
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
            //Debug.Log(_watchersCount);
            if (_watchersCount == 1)
            {
                IsVisible = true;
                _visibleTimer.SetPause();
                OnVisibilityChanged?.Invoke(IsVisible);
            }
        }

        public void RemoveWatcher()
        {
            _watchersCount--;

#if UNITY_EDITOR
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
        }
    }
}
