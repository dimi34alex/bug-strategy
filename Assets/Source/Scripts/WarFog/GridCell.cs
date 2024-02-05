using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Grid cell component. Handles mouse interations with the cell
/// </summary>
public class GridCell : MonoBehaviour
{
    /// <summary>
    /// Ranges from 0 to 1 with 0 indicating that the cell is not visible
    /// </summary>

    public float Visibility;
    public bool isVisible = false;
    private int _cnt;

    /// <summary>
    /// Register this cell in the mask rendering script
    /// </summary>
    private void Start()
    {
        MaskRenderer.RegisterCell(this);
        _cnt = 0;
    }

    /// <summary>
    /// Make sure to toggle visiblity either when clicked on...
    /// </summary>
    public void SetVisibility(bool visible)
    {
        if (isVisible != visible)
        {
            isVisible = visible;
            StopAllCoroutines();
            StartCoroutine(AnimateVisibility(isVisible ? 1.0f : 0.0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            _cnt++;
            SetVisibility(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверьте, что объект, покинувший триггер, является юнитом
        if (other.gameObject.CompareTag("Player"))
        {
            _cnt--;
            if (_cnt > 0)
                return;
            Debug.Log("End of obj" + other.gameObject.name);
            // Если это юнит, делаем эту ячейку невидимой
            SetVisibility(false);
        }
    }
    /// <summary>
    /// Visibility toggle animation
    /// Pretty basic animation coroutine, the animation takes 1 second
    /// </summary>
    /// <param name="targetVal">Visibility value to end up with</param>
    /// <returns>Yield</returns>
    private IEnumerator AnimateVisibility(float targetVal)
    {
        float startingTime = Time.time;
        float startingVal = Visibility;
        float lerpVal = 0.0f;
        while(lerpVal < 1.0f)
        {
            lerpVal = (Time.time - startingTime) / 1.0f;
            Visibility = Mathf.Lerp(startingVal, targetVal, lerpVal);
            if (gameObject.name == "XDD")
            {
                Debug.Log("Starting time " + startingTime);
                Debug.Log("startingVal" + startingVal);
                Debug.Log("lerp val" + lerpVal);
            }

            yield return null;
        }
        Visibility = targetVal;
    }
}