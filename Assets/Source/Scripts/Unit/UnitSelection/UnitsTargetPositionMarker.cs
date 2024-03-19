using System;
using System.Collections;
using UnityEngine;

public class UnitsTargetPositionMarker : MonoBehaviour, IPoolable<UnitsTargetPositionMarker>, IPoolEventListener
{
    [SerializeField] private float time;
    [SerializeField] private SpriteRenderer sprite;
    private float _timer = 0;

    public event Action<UnitsTargetPositionMarker> ElementReturnEvent;
    public event Action<UnitsTargetPositionMarker> ElementDestroyEvent;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        ResetData();
    }

    public void OnElementExtract()
    {
        gameObject.SetActive(true);
        StartCoroutine(Tick());
    }
    
    public void OnElementReturn()
    {
        gameObject.SetActive(false);
        ResetData();
    }
    
    IEnumerator Tick()
    {
        while (_timer < time)
        {
            Color color = sprite.color;
            color.a = 1 - (_timer / time);
            sprite.color = color; 
            
            yield return new WaitForEndOfFrame();
            _timer += Time.deltaTime;
        }
        
        ElementReturnEvent?.Invoke(this);
    }
    
    private void ResetData()
    {
        _timer = 0;
            
        Color color = sprite.color;
        color.a = 1;
        sprite.color = color;
    }
    
    private void OnDestroy()
    {
        ElementDestroyEvent?.Invoke(this);
    }
}