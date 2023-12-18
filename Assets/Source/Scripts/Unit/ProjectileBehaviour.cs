using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour, IDamageApplicator, IPoolable<ProjectileBehaviour>
{
    private Transform _target;
    private IDamagable _damagable;

    public float speed = 1f;

    public float damageAmount;

    public event System.Action<ProjectileBehaviour> ElementReturnEvent;
    public event System.Action<ProjectileBehaviour> ElementDestroyEvent;

    public float Damage { get; set; }

    private void Start()
    {
        Damage = damageAmount;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
    }

    public void TargetLock(Transform target)
    {
        _target = target;
        _damagable = target.GetComponent<IDamagable>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_damagable!=null && (collider.gameObject.tag == "Unit" ||
            collider.gameObject.tag == "Worker" || collider.gameObject.tag == "Building"))
        {
            if (collider.gameObject.GetComponent<IDamagable>() == _damagable)
            {
                _damagable.TakeDamage(this);
                ElementReturnEvent?.Invoke(this);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        ElementDestroyEvent?.Invoke(this);
    }
}
