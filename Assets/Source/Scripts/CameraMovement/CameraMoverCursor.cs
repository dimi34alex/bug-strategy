using CycleFramework.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy
{
    public class CameraMoverCursor : MonoBehaviour
    {
        [SerializeField] private float CameraMoveSpeed;
        [SerializeField] private float PercentageOfScreen;
        [SerializeField] private GameObject field;

        private float SizeBorderToMove;
        private Vector3[] _bounds;

        private void Awake ()
        {
            _bounds = new Vector3[2];
            _bounds[0] = new Vector3(field.transform.localScale.x * -5f, -100f, field.transform.localScale.z * -5f);
            _bounds[1] = new Vector3(field.transform.localScale.x * 5f, 100f, field.transform.localScale.z * 5f);
            SizeBorderToMove = Mathf.Min(Screen.width, Screen.height) / PercentageOfScreen;
        }

        private void Update ()
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;

            Vector2 delta = mousePos - new Vector2(0.5f, 0.5f);

            float xDist = Screen.width * (0.5f - Mathf.Abs(delta.x));
            float yDist = Screen.height * (0.5f - Mathf.Abs(delta.y));

            if(xDist < SizeBorderToMove || yDist < SizeBorderToMove)
            {
                delta = delta.normalized;
                delta *= Mathf.Clamp01(1 - Mathf.Min(xDist, yDist) / SizeBorderToMove);

                transform.Translate(delta * CameraMoveSpeed * Time.deltaTime, Space.Self);
                transform.position = transform.position.Clamp(_bounds[0], _bounds[1]);
            }
        }
    }
}
