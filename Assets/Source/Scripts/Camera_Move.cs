using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private int screenWidth;
    private int screenHeight;
    public float speed;
    public bool useCameraMovement;


    void Start()
    {
        speed = 100;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }


    void Update()
    {
        Vector3 camPos  = transform.position;

        if( Input.mousePosition.x <= 5) // Лево
        {
            if( Input.mousePosition.x <= 10 && Input.mousePosition.y >= screenHeight - 10 ) // Лево Вверх
            {
                camPos.x -= Time.deltaTime * speed;  
                camPos.z += Time.deltaTime * speed;  
            }

            else if ( Input.mousePosition.x <= 10 && Input.mousePosition.y <= 10 ) // Лево Вниз
            {
                camPos.x -= Time.deltaTime * speed; 
                camPos.z -= Time.deltaTime * speed;
            }

            else
            {
                camPos.x -= Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);
            }
        }


        else if( Input.mousePosition.x >= screenWidth - 5) // Право
        {
            if( Input.mousePosition.x >= screenWidth - 10 && Input.mousePosition.y >= screenHeight - 10 ) // Право Вверх
            {
                camPos.x += Time.deltaTime * speed;  
                camPos.z += Time.deltaTime * speed;  
            }

            else if ( Input.mousePosition.x >= screenWidth - 10 && Input.mousePosition.y <= 10 ) // Право Вниз
            {
                camPos.x += Time.deltaTime * speed; 
                camPos.z -= Time.deltaTime * speed;
            }

            else
            {
                camPos.x += Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed); 
            }          
        }

        else if( Input.mousePosition.y <= 5) // Вниз
        {
            camPos.z -= Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);            
        }

        else if( Input.mousePosition.y >= screenHeight - 5) // Вверх
        {
            camPos.z += Time.deltaTime * Mathf.Sqrt(speed*speed + speed*speed);            
        }

        transform.position = camPos;
    }
}

