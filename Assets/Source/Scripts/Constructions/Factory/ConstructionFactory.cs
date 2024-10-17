using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.Constructions.Factory.Behaviours;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.Factory
{
    public interface IConstructionFactory
    {
        public TConstruction Create<TConstruction>(ConstructionID constructionID, Vector3 position, AffiliationEnum affiliation) 
            where TConstruction : ConstructionBase;

        public event Action<ConstructionBase> Created;
    }

    public class ConstructionFactory : MonoBehaviour, IConstructionFactory
    {
        [Inject] private readonly ConstructionsRepository _repository;
        [Inject] private readonly ConstructionTypeMatchConfig _constructionTypeMatchConfig;

        private IReadOnlyDictionary<ConstructionType, ConstructionFactoryBehaviourBase> _behaviours;

        public event Action<ConstructionBase> Created;

        private void Awake()
        {
            _behaviours = GetComponentsInChildren<ConstructionFactoryBehaviourBase>(true)
                .ToDictionary(behaviour => behaviour.ConstructionType, behaviour => behaviour);

            foreach (ConstructionFactoryBehaviourBase behaviour in _behaviours.Values)
                Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
        }

        public TConstruction Create<TConstruction>(ConstructionID constructionID, Vector3 position, AffiliationEnum affiliation) 
            where TConstruction : ConstructionBase
        {
            ConstructionType constructionType = _constructionTypeMatchConfig.GetConstructionType(constructionID);

            if (!_behaviours.ContainsKey(constructionType))
                throw new InvalidOperationException($"{constructionID} cannot be created, " +
                                                    $"because factory for this construction not found. Create new factory behaviour for this construction");

            var construction = _behaviours[constructionType].Create<TConstruction>(constructionID);
            construction.Initialize(affiliation);
            construction.transform.position = position;
            _repository.AddConstruction(position, construction);
            Created?.Invoke(construction);
            return construction;
        }
    }
}