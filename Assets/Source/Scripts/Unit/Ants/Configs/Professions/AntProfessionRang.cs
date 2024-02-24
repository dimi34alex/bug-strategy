using System;
using UnityEngine;

namespace Unit.Ants.Configs.Professions
{
    [Serializable]
    public struct AntProfessionRang
    {
        private const int MinValue = 0;
        private const int MaxValue = 3;

        [field: SerializeField] [field: Range(MinValue, MaxValue)] public int Rang { get; private set; }

        public AntProfessionRang(int rang) => Rang = Mathf.Clamp(rang, MinValue, MaxValue);
    }
}