using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBehaviour : MonoBehaviour, IDamageApplicator
{
    public float attackDelay;
    public float delayTimer;

    public float damageAmount;

    public Affiliation unitsTeam;
    public GameObject projectilePrefab;

    public float Damage { get; set; }

    void Start()
    {
        unitsTeam = this.gameObject.transform.parent.GetComponent<Affiliation>();

        Damage = damageAmount;
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Unit" || collider.gameObject.tag == "Worker")
        {

            if (!(collider.gameObject.GetComponent<Affiliation>().affiliation == unitsTeam.affiliation))
            {
                delayTimer += Time.deltaTime;
                if (attackDelay < delayTimer)
                {
                    delayTimer = 0;
                    AttackUnit(collider.gameObject.GetComponent<MovingUnit>());
                }
            }
        }
        else if (collider.gameObject.tag == "Building")
        {
            if (!(collider.gameObject.GetComponent<Affiliation>().affiliation == unitsTeam.affiliation))
            {
                delayTimer += Time.deltaTime;
                if (attackDelay < delayTimer)
                {
                    delayTimer = 0;
                    AttackBuilding(collider.gameObject.GetComponent<ConstructionBase>());
                }
            }
        }

    }

    public void AttackUnit(UnitBase target)
    {
        Debug.Log("Юнит " + this.gameObject.transform.parent.name + " атакует " + target.gameObject.name + " на " + Damage + " ОЗ");
     
        GameObject projectile;

        projectile = GameObject.Instantiate(projectilePrefab, this.gameObject.transform.position, projectilePrefab.transform.rotation);
        ProjectileBehaviour projectileBehaviour = projectile.AddComponent<ProjectileBehaviour>();
        projectile.gameObject.GetComponent<ProjectileBehaviour>().TargetLockUnit(target);

    }

    public void AttackBuilding(ConstructionBase target)
    {
        Debug.Log("Юнит " + this.gameObject.transform.parent.name + " атакует " + target.gameObject.name + " на " + Damage + " ОЗ");

        GameObject projectile;

        projectile = GameObject.Instantiate(projectilePrefab, this.gameObject.transform.position, projectilePrefab.transform.rotation);

        projectile.gameObject.GetComponent<ProjectileBehaviour>().TargetLockBuilding(target);
    }
}
