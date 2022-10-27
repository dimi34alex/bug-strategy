using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Bee_TownHall : MonoBehaviour
{
    void Start()
    {
        TownHall.WorkerBeeAlarmOn.AddListener(GoHome);
    }

    void GoHome(){
        Debug.Log("Go home");
        TownHall._HideMe(gameObject);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - 0.1F, transform.position.y, transform.position.z);
    }
}
