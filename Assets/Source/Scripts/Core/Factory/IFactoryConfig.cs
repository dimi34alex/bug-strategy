using System.Collections.Generic;

namespace BugStrategy.Factory
{
    public interface IFactoryConfig<TId, TPrefab>
    {
        public IReadOnlyDictionary<TId, TPrefab> GetData();
    }
}