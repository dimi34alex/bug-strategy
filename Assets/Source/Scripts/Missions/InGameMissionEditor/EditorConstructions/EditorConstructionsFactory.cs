using BugStrategy.Constructions;
using BugStrategy.Factory;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.EditorConstructions
{
    public class EditorConstructionsFactory : ObjectsFactoryBase<ConstructionID, EditorConstruction>
    {
        public EditorConstructionsFactory(DiContainer diContainer, EditorConstructionsConfig config) 
            : base(diContainer, config, "EditorConstructions") { }
    }
}