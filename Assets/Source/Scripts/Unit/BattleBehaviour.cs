using UnityEngine;

public class BattleBehaviour : MonoBehaviour, IDamageApplicator
{
    public float attackDelay;
    public float delayTimer;

    public float damageAmount;

    public AffiliationEnum unitsTeam;
    public GameObject projectilePrefab;

    public float Damage => damageAmount;

    private Pool<ProjectileBehaviour> _projectilePool;

    private void Awake()
    {
        unitsTeam = gameObject.transform.parent.GetComponent<IAffiliation>().Affiliation;

        _projectilePool = new Pool<ProjectileBehaviour>(CreateProjectileBehaviour);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Unit" || collider.gameObject.tag == "Worker" || collider.gameObject.tag == "Building")
        {
            if (!(collider.gameObject.GetComponent<IAffiliation>().Affiliation == unitsTeam))
            {
                delayTimer += Time.deltaTime;
                if (attackDelay < delayTimer)
                {
                    delayTimer = 0;
                    Attack(collider.transform);
                }
            }
        }
    }

    public void Attack(Transform target)
    {
        Debug.Log("���� " + this.gameObject.transform.parent.name + " ������� " + target.gameObject.name + " �� " + Damage + " ��");

        _projectilePool.ExtractElement().TargetLock(target);
    }

    private ProjectileBehaviour CreateProjectileBehaviour()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, this.gameObject.transform.position,
            projectilePrefab.transform.rotation);

        return projectile.AddComponent<ProjectileBehaviour>();
    }
}
