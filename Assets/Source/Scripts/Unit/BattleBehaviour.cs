using UnityEngine;

public class BattleBehaviour : MonoBehaviour, IDamageApplicator
{
    public float attackDelay;
    public float delayTimer;

    public float damageAmount;

    public AffiliationEnum unitsTeam;
    public GameObject projectilePrefab;

    public float Damage { get; set; }

    private void Start()
    {
        unitsTeam = gameObject.transform.parent.GetComponent<IAffiliation>().Affiliation;

        Damage = damageAmount;
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Unit" || collider.gameObject.tag == "Worker")
        {

            if (!(collider.gameObject.GetComponent<IAffiliation>().Affiliation == unitsTeam))
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
            if (!(collider.gameObject.GetComponent<IAffiliation>().Affiliation == unitsTeam))
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
        Debug.Log("���� " + this.gameObject.transform.parent.name + " ������� " + target.gameObject.name + " �� " + Damage + " ��");
     
        GameObject projectile;

        projectile = GameObject.Instantiate(projectilePrefab, this.gameObject.transform.position, projectilePrefab.transform.rotation);
        ProjectileBehaviour projectileBehaviour = projectile.AddComponent<ProjectileBehaviour>();
        projectile.gameObject.GetComponent<ProjectileBehaviour>().TargetLockUnit(target);

    }

    public void AttackBuilding(ConstructionBase target)
    {
        Debug.Log("���� " + this.gameObject.transform.parent.name + " ������� " + target.gameObject.name + " �� " + Damage + " ��");

        GameObject projectile;

        projectile = GameObject.Instantiate(projectilePrefab, this.gameObject.transform.position, projectilePrefab.transform.rotation);

        projectile.gameObject.GetComponent<ProjectileBehaviour>().TargetLockBuilding(target);
    }
}
