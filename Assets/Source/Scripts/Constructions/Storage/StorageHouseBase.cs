using Constructions.LevelSystemCore;

namespace Constructions
{
    public abstract class StorageHouseBase : ConstructionBase, IEvolveConstruction
    {
        public abstract IConstructionLevelSystem LevelSystem { get; protected set; }
    }
}