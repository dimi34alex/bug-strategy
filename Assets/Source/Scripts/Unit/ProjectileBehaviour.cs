using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour, IDamageApplicator
{
    public UnitBase objectiveUnit;
    public ConstructionBase objectiveBuilding;
    public float speed = 1f;

    public float damageAmount;
    public float Damage { get; set; }

    void Start()
    {
        Damage = damageAmount;
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        if (objectiveUnit)
        {
            transform.position = Vector3.MoveTowards(transform.position, objectiveUnit.gameObject.transform.position, step);
        }
        else if (objectiveBuilding)
        {
            transform.position = Vector3.MoveTowards(transform.position, objectiveBuilding.gameObject.transform.position, step);
        }
    }

    public void TargetLockUnit(UnitBase target)
    {
        objectiveUnit = target;
    }

    public void TargetLockBuilding(ConstructionBase target)
    {
        objectiveBuilding = target;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (objectiveUnit&&(collider.gameObject.tag == "Unit" || collider.gameObject.tag == "Worker"))
        {
            if (collider.gameObject.GetComponent<MovingUnit>() == objectiveUnit)
            {
                objectiveUnit.TakeDamage(this);
                Destroy(this.gameObject);
            }
        }
        else if (objectiveBuilding && (collider.gameObject.tag == "Building"))
        {
            if (collider.gameObject.GetComponent<ConstructionBase>() == objectiveBuilding);
            {
                objectiveBuilding.TakeDamage(this);
                Destroy(this.gameObject);
            }
        }


    }
}
