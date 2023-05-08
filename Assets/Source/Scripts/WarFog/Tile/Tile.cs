using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : TriggerZone, ITriggerable
{
    [SerializeField] private GameObject warFog;
    [SerializeField] private GameObject startFog;
    public bool Visible { get; private set; }
    
    private Func<VisibleWarFogZone, bool> _filter = t => true;
    
    public new event Action<VisibleWarFogZone> EnterEvent;
    public new event Action<VisibleWarFogZone> ExitEvent;
    
    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is VisibleWarFogZone && _filter(t.Cast<VisibleWarFogZone>());
    protected override bool _refreshEnteredComponentsAfterExit => false;

    public bool Contains(VisibleWarFogZone target) => ContainsComponents.Contains(target);
    
    public event Action<MonoBehaviour> OnDestroyEvent;
    public event Action<MonoBehaviour> OnDisableEvent;
    
    private bool _showStartWarFog = true;
    private Coroutine _invisibleTimer;

    private void Awake()
    {
        Visible = false;
    }

    public void SetFilter(Func<VisibleWarFogZone, bool> filter)
    {
        _filter = filter;
    }
    
    protected override void OnEnter(ITriggerable component)
    {
        VisibleWarFogZone watcher = component.Cast<VisibleWarFogZone>();
        
        if (_showStartWarFog)
        {
            _showStartWarFog = false;
            Destroy(startFog);
        }
        
        if (ContainsComponents.Count == 1)
        {
            warFog.SetActive(false);
            Visible = true;
            
            if(_invisibleTimer != null)
                StopCoroutine(_invisibleTimer);
        }
        
        EnterEvent?.Invoke(watcher);
    }

    protected override void OnExit(ITriggerable component)
    {
        VisibleWarFogZone watcher = component.Cast<VisibleWarFogZone>();

        if (!ContainsComponents.Any())
        {
            _invisibleTimer = StartCoroutine(MakeInvisibleCoroutine());
        }
        
        ExitEvent?.Invoke(watcher);
    }

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
    
    private void OnDisable()
    {
        OnDisableEvent?.Invoke(this);
    }
    
    IEnumerator MakeInvisibleCoroutine()
    {
        yield return new WaitForSeconds(5f);
        warFog.SetActive(true);
        Visible = false;
    }
}
