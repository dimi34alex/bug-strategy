using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private int screenWidth;
    private int screenHeight;
    private float fieldWidth;
    private float fieldHeight;
    public float speed;
    public bool useCameraMovement;
    public GameObject field;

    [SerializeField] private bool useScroll;
    [SerializeField] private float scrollPower;
    [SerializeField] private float cameraSizeOffset;
    private Camera _camera;
    private float _startCameraSize;
    
    void Start()
    {
        speed = 50;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        fieldHeight = field.transform.localScale.z*5;
        fieldWidth = field.transform.localScale.x*5;
        // размеры поля для ограничения движения камеры
        
        _camera = GetComponent<Camera>();
        _startCameraSize = _camera.orthographicSize;
    }


    void Update()
    {
        Vector3 camPos  = transform.position;


        if(Input.mousePosition.x <= 5 && camPos.x > -fieldWidth) // Лево
        {
            if( Input.mousePosition.x <= 10 && Input.mousePosition.y >= screenHeight - 10  && camPos.z < fieldHeight) // Лево Вверх
            {
                camPos.x -= Time.deltaTime * speed;  
                camPos.z += Time.deltaTime * speed;  
            }

            else if ( Input.mousePosition.x <= 10 && Input.mousePosition.y <= 10 && camPos.z > -fieldHeight) // Лево Вниз
            {
                camPos.x -= Time.deltaTime * speed; 
                camPos.z -= Time.deltaTime * speed;
            }

            else
            {
                camPos.x -= Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);
            }
        }


        else if(Input.mousePosition.x >= screenWidth - 5 && camPos.x < fieldWidth) // Право
        {
            if( Input.mousePosition.x >= screenWidth - 10 && Input.mousePosition.y >= screenHeight - 10 && camPos.z < fieldHeight) // Право Вверх
            {
                camPos.x += Time.deltaTime * speed;  
                camPos.z += Time.deltaTime * speed;  
            }

            else if ( Input.mousePosition.x >= screenWidth - 10 && Input.mousePosition.y <= 10 && camPos.z > -fieldHeight) // Право Вниз
            {
                camPos.x += Time.deltaTime * speed; 
                camPos.z -= Time.deltaTime * speed;
            }

            else
            {
                camPos.x += Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed); 
            }          
        }

        else if( Input.mousePosition.y <= 5 && camPos.z > -fieldHeight) // Вниз
        {
            camPos.z -= Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);            
        }

        else if( Input.mousePosition.y >= screenHeight - 5 && camPos.z < fieldHeight) // Вверх
        {
            camPos.z += Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);            
        }

        transform.position = camPos;

        CameraScrolling();
    }

    private void CameraScrolling()
    {
        if (!useScroll)
            return;
        
        float scrollDirection = Input.mouseScrollDelta.y;
        if(scrollDirection == 0)
            return;
        
        float newCameraSize = _camera.orthographicSize;
        newCameraSize -= scrollDirection * 10 * scrollPower * Time.deltaTime;
        newCameraSize = Mathf.Clamp(newCameraSize, _startCameraSize - cameraSizeOffset, _startCameraSize + cameraSizeOffset);
        _camera.orthographicSize = newCameraSize;
    }
}