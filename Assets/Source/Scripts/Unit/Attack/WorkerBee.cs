using UnityEngine;

public class WorkerBee : BeeUnit
{
    private WorkerDuty _workerDuty;

    public override UnitType UnitType => UnitType.WorkerBee;

    private void Start()
    {
        UnitPool.Instance.UnitCreation(this);
        _workerDuty = GetComponentInChildren<WorkerDuty>();
    }

    public override void GiveOrder(GameObject target, Vector3 position)
    {
        if (!IsSelected) return;

        string target_tag = target.tag;

            switch (target_tag)
            {
                case "PollenSource":
                    _workerDuty.isFindingRes = true;
                    _workerDuty.WorkingOnGO = target;
                    break;

                default:
                    _workerDuty.isFindingRes = false;
                    _workerDuty.isGathering = false;
                    _workerDuty.isBuilding = false;
                    _workerDuty.isFindingBuild = false;
                    break;
            }
     
        SetDestination(position);
    }
}
    