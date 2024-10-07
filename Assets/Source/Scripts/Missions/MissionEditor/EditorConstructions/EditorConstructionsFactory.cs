using BugStrategy.Constructions;
using BugStrategy.Factory;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstructionsFactory : ObjectsFactoryBase<ConstructionID, EditorConstruction>
    {
        public EditorConstructionsFactory(DiContainer diContainer, EditorConstructionsConfig config) 
            : base(diContainer, config, "EditorConstructions") { }
    }
}