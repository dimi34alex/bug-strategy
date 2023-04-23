using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Bee_TownHall : MonoBehaviour
{
    void Start()
    {
        TownHall.WorkerBeeAlarmOn += GoHome;
    }

    void GoHome(){
        Debug.Log("Go home");
        TownHall.HideMe(gameObject);
    }

    void FixedUpdate()
    {

    }
}
