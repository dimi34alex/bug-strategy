using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerDuty : MonoBehaviour
{
    public bool isGathering, isFindingRes, gotResource, isFindingBuild, isBuilding;
    private UI_GameplayMain UI_Counter;
    private Vector3 destination;
    private Transform ResourceSkin;

    void Start()
    {
        isGathering = false;
        UI_Counter = GameObject.Find("UI_GameplayMain").GetComponent<UI_GameplayMain>();
        
        ResourceSkin = gameObject.transform.GetChild(2);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "PollenSource":
                {
                    if (isFindingRes)
                    {
                        isGathering = true;
                        Vector3 workplace = collider.gameObject.transform.position;
                        StartCoroutine(GatheringCourutine(workplace));
                    }
                }
                break;

            case "BuildMark":
                {
                    isBuilding = true;
                    isFindingBuild = false;
                    collider.gameObject.GetComponent<BuildingProgressConstruction>().WorkerArrived = true;
                }
                break;

            default:
                {
                    if (gotResource && collider.gameObject.name == "TownHall")
                    {
                        gotResource = false;
                        ResourceSkin.transform.gameObject.SetActive(false);
                        isFindingRes = true;
                        UI_Counter.GatheringPollen(10);

                        this.GetComponent<MovingUnit>().SetDestination(destination);
                    }
                }
                break;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "BuildMark")
        {
            collider.gameObject.GetComponent<BuildingProgressConstruction>().WorkerArrived = false;
        }
    }

    IEnumerator GatheringCourutine(Vector3 workplace)
    {
        int timer = 3;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }

        yield return new WaitForSeconds(1f);

        if (isGathering)
        {
            gotResource = true;
            ResourceSkin.transform.gameObject.SetActive(true);
            isFindingRes = false;
            isGathering = false;
            GameObject Base = GameObject.Find("TownHall");
            destination = Base.transform.position;
            this.GetComponent<MovingUnit>().SetDestination(destination);
            destination = workplace;
        }

    }
}
