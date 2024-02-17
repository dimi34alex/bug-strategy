using UnityEngine;

public class WorkerDuty : MonoBehaviour
{
    public bool isGathering, isFindingRes, gotResource, isFindingBuild, isBuilding;
    public Vector3 destination;
    public int loadCapacity;
    private Transform ResourceSkin;
    public GameObject WorkingOnGO;

    public float GatherTimer;

    private void Start()
    {
        isGathering = false;
      
        ResourceSkin = gameObject.transform.parent.GetChild(2);
    }

    private void OnTriggerEnter(Collider collider)
    {

        switch (collider.gameObject.tag)
        {

            case "PollenSource":
                {

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
                        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, loadCapacity);

                        this.gameObject.transform.parent.GetComponent<MovingUnit>().SetDestination(destination);
                    }
                }
                break;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == WorkingOnGO)
            GatherTimer = 0;
      
        if (collider.gameObject.tag == "BuildMark")
            collider.gameObject.GetComponent<BuildingProgressConstruction>().WorkerArrived = false;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject == WorkingOnGO && !collider.transform.parent.GetComponent<PollenStorage>().isPollinated)
        {
            if (isFindingRes && !isGathering)
                isGathering = true;
          

            if (isGathering)
                GatherTimer += Time.deltaTime;
          

            if (GatherTimer > 3 && isGathering)
            {
                gotResource = true;
                ResourceSkin.transform.gameObject.SetActive(true);
                isFindingRes = false;
                isGathering = false;

                collider.transform.parent.GetComponent<PollenStorage>().ExtractResource(loadCapacity);

                GameObject Base = GameObject.Find("TownHall");
                destination = Base.transform.position;
                this.gameObject.transform.parent.GetComponent<MovingUnit>().SetDestination(destination);
                destination = WorkingOnGO.transform.position;
            }
        }
    }
}
