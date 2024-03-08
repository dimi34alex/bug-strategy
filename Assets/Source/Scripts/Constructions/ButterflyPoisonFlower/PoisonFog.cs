using DG.Tweening;
using UnityEngine;

public class PoisonFog : MonoBehaviour
{
    [SerializeField] private TriggerBehaviour triggerBehaviour;
    [SerializeField] private SphereCollider triggerCollider;
    [SerializeField] private GameObject skinParent;

    private Sequence _fogExistTimer;
    
    private void Start()
    {
        triggerBehaviour.EnterEvent += OnUnitEnter;
        triggerBehaviour.ExitEvent += OnUnitExit;
    }

    public void Init(float fogRadius, float fogExistTime)
    {
        triggerCollider.radius = fogRadius;
        skinParent.transform.localScale = new Vector3(fogRadius, fogRadius, fogRadius);
        _fogExistTimer = DOTween.Sequence()
            .SetUpdate(UpdateType.Manual)
            .AppendInterval(fogExistTime)
            .AppendCallback(RemoveFog);
    }


    private void Update()
    {
        float time = Time.deltaTime;
        _fogExistTimer.ManualUpdate(time, time);
    }

    private void RemoveFog()
    {
        Destroy(gameObject);
    }
    
    private void OnUnitEnter(ITriggerable triggerable)
    {
        if (triggerable.TryCast(out IPoisoneable poisoneable))
            poisoneable.PoisonProcessor.Poised(true);
    }

    private void OnUnitExit(ITriggerable triggerable)
    {
        if (triggerable.TryCast(out IPoisoneable poisoneable)) 
            poisoneable.PoisonProcessor.OutFromPoisonFog();
    }
}
