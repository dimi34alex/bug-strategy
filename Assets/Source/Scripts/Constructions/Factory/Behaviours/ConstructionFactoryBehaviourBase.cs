using UnityEngine;
using Zenject;

public abstract class ConstructionFactoryBehaviourBase : MonoBehaviour
{
    [Inject] protected readonly DiContainer DiContainer;

    protected void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit() { }

    public abstract ConstructionType ConstructionType { get; }
    public abstract TConstruction Create<TConstruction>(ConstructionID constructionID) where TConstruction : ConstructionBase;
}
