using System;
using System.Collections.Generic;
using CycleFramework.Extensions;

namespace BugStrategy.PoisonFog
{
    public class PoisonFogsRepository
    {
        private readonly List<PoisonFogBehaviour> _fogs = new List<PoisonFogBehaviour>(8);

        public IReadOnlyList<PoisonFogBehaviour> Fogs => _fogs;

        public void Add(PoisonFogBehaviour fog)
        {
            fog.ElementReturnEvent += Remove;

            if (_fogs.Contains(fog))
                throw new Exception("Duplicate");
            
            _fogs.Add(fog);
        }

        public void Remove(PoisonFogBehaviour fog)
            => _fogs.Remove(fog);
        
        public PoisonFogBehaviour TryGet(Predicate<PoisonFogBehaviour> predicate = null, bool remove = false) 
        {
            int index = _fogs.IndexOf(fog => fog.CastPossible<PoisonFogBehaviour>() && (predicate is null || predicate(fog.Cast<PoisonFogBehaviour>())));

            if (index is -1)
                return default;

            PoisonFogBehaviour poisonFog = _fogs[index].Cast<PoisonFogBehaviour>();

            if (remove)
                _fogs.RemoveAt(index);

            return poisonFog;
        }
    }
}