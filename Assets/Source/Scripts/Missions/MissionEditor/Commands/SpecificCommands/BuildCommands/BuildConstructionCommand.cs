using System;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Factories;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class BuildConstructionCommand : ICommand
    {
        public bool IsExecuted { get; private set; }

        private ConstructionID _tileIdReplaced;
        private AffiliationEnum _tileAffiliationReplaced;
        private readonly ConstructionID _defaultId;
        private readonly ConstructionID _tileId;
        private readonly AffiliationEnum _tileAffiliation;
        private readonly Vector3 _point;
        private readonly FactoryWithId<ConstructionID, EditorConstruction> _factory;
        private readonly GridRepository<EditorConstruction> _positionsRepository;

        public event Action<ICommand> OnExecuted;
        
        public BuildConstructionCommand(ConstructionID tileId, 
            AffiliationEnum tileAffiliation, Vector3 point, 
            FactoryWithId<ConstructionID, EditorConstruction> factory, 
            GridRepository<EditorConstruction> positionsRepository)
        {
            _tileIdReplaced = _defaultId = ConstructionID.None;
            _tileId = tileId;
            _tileAffiliation = tileAffiliation;
            _point = point;
            _factory = factory;
            _positionsRepository = positionsRepository;
        }

        public void Execute()
        {
            if (IsExecuted)
                return;

            if (_positionsRepository.Exist(_point, true, false))
            {
                var oldTile = _positionsRepository.Get(_point, true);
                _tileIdReplaced = GetId(oldTile);
                _tileAffiliationReplaced = oldTile.Affiliation;
                Object.Destroy(oldTile.gameObject);
            }

            var newTile = Create(_tileId, _tileAffiliation, _point);
            if (!_positionsRepository.TryAdd(_point, newTile))
                throw new Exception("Tile cant be placed");

            IsExecuted = true;
            OnExecuted?.Invoke(this);
        }

        public void Undo()
        {
            if (!IsExecuted)
                return;

            if (_positionsRepository.Exist(_point, true, false))
            {
                var newTile = _positionsRepository.Get(_point, true);
                Object.Destroy(newTile.gameObject);
            }

            if (ReplacedIdIsValid(_tileIdReplaced))
            {
                var oldTile = Create(_tileIdReplaced, _tileAffiliationReplaced, _point);
                if (!_positionsRepository.TryAdd(_point, oldTile))
                    throw new Exception("Tile cant be placed");
            
                _tileIdReplaced = _defaultId;
            }

            IsExecuted = false;
        }

        protected virtual EditorConstruction Create(ConstructionID id, AffiliationEnum affiliation, Vector3 point)
        {
            var tile = _factory.Create(id, point);
            tile.Initialize(affiliation);
            return tile;
        }

        private static ConstructionID GetId(EditorConstruction tile) 
            => tile.constructionID;

        private static bool ReplacedIdIsValid(ConstructionID replacedId) 
            => replacedId != ConstructionID.None;
    }
}