using System;
using System.Collections.Generic;
using Avastrad.EventBusFramework;
using BugStrategy.Constructions;
using BugStrategy.Events;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    public class TilesShadower : IDisposable, IEventReceiver<EventTileVisibilityChanged>
    {
        private readonly IEventBus _eventBus;
        private readonly TileShadowFactory _tileShadowFactory;
        private readonly TilesShadowerConfig _config;
        private readonly ConstructionsRepository _constructionsRepository;
        private readonly ResourceSourcesRepository _resourceSourcesRepository;

        private readonly Dictionary<GridKey3, TileShadow> _existShadows = new(32);

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        public TilesShadower(IEventBus eventBus, TileShadowFactory tileShadowFactory, TilesShadowerConfig config, ConstructionsRepository constructionsRepository, ResourceSourcesRepository resourceSourcesRepository)
        {
            _eventBus = eventBus;
            _tileShadowFactory = tileShadowFactory;
            _config = config;
            _constructionsRepository = constructionsRepository;
            _resourceSourcesRepository = resourceSourcesRepository;
            _eventBus.Subscribe(this);
        }
        
        public void OnEvent(EventTileVisibilityChanged e)
        {
            if (e.IsVisible)
                TryRemoveShadow(e.Position);
            else
                TrySetShadow(e.Position);
        }

        private void TrySetShadow(Vector3 position)
        {
            if (_existShadows.ContainsKey(position))
                throw new Exception($"Position {position} already exist in grid");

            if (TryGetView(position, out var view))
            {
                var shadow = _tileShadowFactory.Create(position);
                shadow.CopyView(view);
                _existShadows.Add(position, shadow);
            }
        }

        private void TryRemoveShadow(Vector3 position)
        {
            if (_existShadows.TryGetValue(position, out var shadow))
            {
                _existShadows.Remove(position);
                shadow.ManualReturnInPool();
            }
        }

        private bool TryGetView(Vector3 position, out ObjectView shadowSprite)
        {
            if (_constructionsRepository.ConstructionExist(position))
            {
                var constr = _constructionsRepository.GetConstruction(position);
                shadowSprite = constr.View;
                return true;
            } 
            
            if (_resourceSourcesRepository.Exist(position))
            {
                var resourceSource = _resourceSourcesRepository.Get(position);
                shadowSprite = resourceSource.View;
                return true;
            }

            shadowSprite = null;
            return false;
        }

        public void Dispose()
        {
            _eventBus?.UnSubscribe(this);
        }
    }
}