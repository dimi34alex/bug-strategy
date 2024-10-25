using System.Collections.Generic;

namespace BugStrategy.Factories
{
    public interface IFactoryConfig<TId, TPrefab>
    {
        public IReadOnlyDictionary<TId, TPrefab> GetData();
    }
}