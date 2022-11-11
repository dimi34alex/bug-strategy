using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ArmyBee_Barrack : MonoBehaviour
{

    private UnitPool pool;

    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        pool = controller.GetComponent<UnitPool>();
        pool.UnitCreation(gameObject);
    }

    void FixedUpdate()
    {
       
    }
}
