using UnityEngine;
using Zenject;

namespace BugStrategy.Factories
{
    public class FactorySinglePool<TResult> : FactorySingle<TResult>
        where TResult : MonoBehaviour, Pool.IPoolable<TResult>
    {
        private readonly Pool.Pool<TResult> _pool;

        protected FactorySinglePool(DiContainer diContainer, TResult prefab, string parentName)
            : base(diContainer, prefab, parentName)
        {
            _pool = new Pool.Pool<TResult>(InstantiateNewObject);
        }

        protected override TResult GetObjectInstance() 
            => _pool.ExtractElement();
    }
}