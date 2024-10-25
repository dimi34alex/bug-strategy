using BugStrategy.Constructions;
using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Missions.MissionEditor.EditorConstructions
{
    public class EditorConstructionsFactory : FactoryWithId<ConstructionID, EditorConstruction>
    {
        public EditorConstructionsFactory(DiContainer diContainer, EditorConstructionsConfig config) 
            : base(diContainer, config, "EditorConstructions") { }
    }
}