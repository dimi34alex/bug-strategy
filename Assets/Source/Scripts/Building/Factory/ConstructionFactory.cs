using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public interface IConstructionFactory
{
    public TConstruction Create<TConstruction>(ConstructionID constructionID) where TConstruction : ConstructionBase;
}

public class ConstructionFactory : MonoBehaviour, IConstructionFactory
{
    [Inject] private readonly ConstructionTypeMatchConfig _constructionTypeMatchConfig;

    private IReadOnlyDictionary<ConstructionType, ConstructionFactoryBehaviourBase> _behaviours;

    private void Awake()
    {
        _behaviours = GetComponentsInChildren<ConstructionFactoryBehaviourBase>(true)
            .ToDictionary(behaviour => behaviour.ConstructionType, behaviour => behaviour);

        foreach (ConstructionFactoryBehaviourBase behaviour in _behaviours.Values)
            Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
    }

    public TConstruction Create<TConstruction>(ConstructionID constructionID) where TConstruction : ConstructionBase
    {
        ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

        if (!_behaviours.ContainsKey(constructionType))
            throw new InvalidOperationException($"{constructionID} cannot be created, " +
                $"because factory for this construction not found. Create new factory behavoiur for this construction");

        return _behaviours[constructionType].Create<TConstruction>(constructionID);
    }
}
