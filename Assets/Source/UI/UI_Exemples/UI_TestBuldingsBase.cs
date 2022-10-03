using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TestBuldingsBase : MonoBehaviour
{

    Camera cam;
    [SerializeField] LayerMask layerMask;
    bool setBuild = true;
    void Awake()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        _MoveBuild();
    }

    void _MoveBuild()
    {
        if (setBuild)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100F, layerMask))
            {
                transform.position = ray.GetPoint(hit.distance);
                if (Input.GetButtonDown("Fire1"))//лкм
                    setBuild = false;
            }

            if (Input.GetButtonDown("Fire2"))//пкм
            {
                Destroy(this.gameObject);
                setBuild = !setBuild;
            }
        }
    }
}
