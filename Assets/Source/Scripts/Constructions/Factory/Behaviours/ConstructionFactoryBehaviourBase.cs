using UnityEngine;

public abstract class ConstructionFactoryBehaviourBase : MonoBehaviour
{
    protected void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit() { }

    public abstract ConstructionType ConstructionType { get; }
    public abstract TConstruction Create<TConstruction>(ConstructionID constructionID) where TConstruction : ConstructionBase;
}
