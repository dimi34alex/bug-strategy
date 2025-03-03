using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.NotConstructions.Factory.Behaviours;
using UnityEngine;
using Zenject;

namespace BugStrategy.NotConstructions.Factory
{
    public interface INotConstructionFactory
    {
        public TConstruction Create<TConstruction>(NotConstructionID notConstructionID, Vector3 position, AffiliationEnum affiliation) 
            where TConstruction : NotConstructionBase;

        public event Action<NotConstructionBase> Created;
    }

    public class NotConstructionFactory : MonoBehaviour, INotConstructionFactory
    {
        [Inject] private readonly NotConstructionsRepository _repository;
        [Inject] private readonly NotConstructionTypeMatchConfig _notConstructionTypeMatchConfig;

        private IReadOnlyDictionary<NotConstructionType, NotConstructionFactoryBehaviourBase> _behaviours;
        private Transform _parent;
        
        public event Action<NotConstructionBase> Created;

        private void Awake()
        {
            _parent = new GameObject()
            {
                transform = { name = "Constructions" }
            }.transform;
            
            _behaviours = GetComponentsInChildren<NotConstructionFactoryBehaviourBase>(true)
                .ToDictionary(behaviour => behaviour.NotConstructionType, behaviour => behaviour);

            //foreach (ConstructionFactoryBehaviourBase behaviour in _behaviours.Values)
            //    Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
        }

        public TConstruction Create<TConstruction>(NotConstructionID notConstructionID, Vector3 position, AffiliationEnum affiliation) 
            where TConstruction : NotConstructionBase
        {
            NotConstructionType notConstructionType = _notConstructionTypeMatchConfig.GetNotConstructionType(notConstructionID);

            if (!_behaviours.ContainsKey(notConstructionType))
                throw new InvalidOperationException($"{notConstructionID} cannot be created, " +
                                                    $"because factory for this construction not found. Create new factory behaviour for this construction");

            var notConstruction = _behaviours[notConstructionType].Create<TConstruction>(notConstructionID, _parent);
            notConstruction.Initialize(affiliation);
            notConstruction.transform.position = position;
            _repository.AddNotConstruction(position, notConstruction);
            Created?.Invoke(notConstruction);
            return notConstruction;
        }
    }
}