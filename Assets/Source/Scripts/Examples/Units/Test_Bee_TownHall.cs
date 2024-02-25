using System.Collections;
using System.Collections.Generic;
using Constructions;
using UnityEngine;

public class Test_Bee_TownHall : MonoBehaviour
{
    void Start()
    {
        BeeTownHall.WorkerBeeAlarmOn += GoHome;
    }

    void GoHome(){
        Debug.Log("Go home");
        BeeTownHall.HideMe(gameObject);
    }

    void FixedUpdate()
    {

    }
}
