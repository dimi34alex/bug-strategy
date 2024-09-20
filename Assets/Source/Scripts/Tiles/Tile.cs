using System;
using BugStrategy.CustomTimer;
using BugStrategy.Trigger;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class Tile : MonoBehaviour, ITriggerable
    {
        [SerializeField] private GameObject warFog;
        [SerializeField] private GameObject startFog;

        private bool _showStartWarFog = true;
        private int _watchersCount;
        private Timer _invisibleTimer;

        public bool Visible { get; private set; }
        public event Action<ITriggerable> OnDisableITriggerableEvent;
        public event Action<Tile> OnDestroyed;

        private void Awake()
        {
            _invisibleTimer = new Timer(5, 0, true);
            _invisibleTimer.OnTimerEnd += HideTile;
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        public void HandleUpdate(float deltaTime) 
            => _invisibleTimer.Tick(deltaTime);

        public void AddWatcher()
        {
            _watchersCount++;
        
            if (_showStartWarFog)
            {
                _showStartWarFog = false;
                Destroy(startFog);
            }
        
            if (_watchersCount == 1)
            {
                warFog.SetActive(false);
                Visible = true;

                _invisibleTimer.SetPause();
            }
        }

        public void RemoveWatcher()
        {
            _watchersCount--;

#if UNITY_EDITOR
            if(_watchersCount < 0) 
                Debug.LogError("_watchersCount is " + _watchersCount);
#endif
        
            if (_watchersCount == 0 && gameObject.activeInHierarchy) 
                _invisibleTimer.Reset();
        }
        
        private void HideTile()
        {
            warFog.SetActive(true);
            Visible = false;
        }
    
        private void OnDisable()
        {
            OnDisableITriggerableEvent?.Invoke(this);
        }
    }
}
