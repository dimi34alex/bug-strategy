using System;
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour, ITriggerable
{
    [SerializeField] private GameObject warFog;
    [SerializeField] private GameObject startFog;

    private bool _showStartWarFog = true;
    private Coroutine _invisibleTimer;
    private int _watchersCount;

    public bool Visible { get; private set; }
    public event Action<ITriggerable> OnDisableITriggerableEvent;
    
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
            
            if(_invisibleTimer != null)
                StopCoroutine(_invisibleTimer);
        }
    }

    public void RemoveWatcher()
    {
        _watchersCount--;

#if UNITY_EDITOR
        if(_watchersCount < 0) Debug.LogError("_watchersCount is " + _watchersCount);
#endif
        
        if (_watchersCount == 0 && gameObject.activeInHierarchy)
        {
            _invisibleTimer = StartCoroutine(MakeTileInvisibleCoroutine());
        }
    }
    
    IEnumerator MakeTileInvisibleCoroutine()
    {
        yield return new WaitForSeconds(5f);
        warFog.SetActive(true);
        Visible = false;
    }
    
    private void OnDisable()
    {
        OnDisableITriggerableEvent?.Invoke(this);
    }
}
