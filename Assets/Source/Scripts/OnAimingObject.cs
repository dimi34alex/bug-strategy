using UnityEngine;
using UnityEngine.EventSystems;

public class OnAimingObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material _material;
    private void Awake()
    {
        if (gameObject.GetComponent<Renderer>()!= null)
            _material = gameObject.GetComponent<Renderer>().material;
        else
            _material = gameObject.GetComponentInChildren<Renderer>().material;
      
        _material.SetInt("_Enable", 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _material.SetInt("_Enable", 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            _material.SetInt("_Enable", 1);
    }
}
