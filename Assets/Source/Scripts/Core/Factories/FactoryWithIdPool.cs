using BugStrategy.Pool;
using UnityEngine;
using Zenject;

namespace BugStrategy.Factories
{
    public class FactoryWithIdPool<TId, TResult> : FactoryWithId<TId, TResult>
        where TResult : MonoBehaviour, Pool.IPoolable<TResult, TId>
    {
        private readonly Pool<TResult, TId> _pool;

        protected FactoryWithIdPool(DiContainer diContainer, IFactoryConfig<TId, TResult> config, string parentName)
            : base(diContainer, config, parentName)
        {
            _pool = new Pool<TResult, TId>(InstantiateNewObject);
        }

        protected override TResult GetObjectInstance(TId id) 
            => _pool.ExtractElement(id);
    }
}