using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ArmyBee_Barrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - 0.1F, transform.position.y, transform.position.z);
    }
}
