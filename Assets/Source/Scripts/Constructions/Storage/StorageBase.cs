using BugStrategy.Constructions.ConstructionLevelSystemCore;

namespace BugStrategy.Constructions
{
    public abstract class StorageBase : ConstructionBase, IEvolveConstruction
    {
        public abstract IConstructionLevelSystem LevelSystem { get; protected set; }
    }
}