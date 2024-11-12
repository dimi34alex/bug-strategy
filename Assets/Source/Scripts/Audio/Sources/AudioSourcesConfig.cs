using System;
using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Audio.Sources
{
    [CreateAssetMenu(fileName = nameof(AudioSourcesConfig), menuName = "Configs/Audio/" + nameof(AudioSourcesConfig))]
    public class AudioSourcesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<AudioSourceType, AudioCell> data;

        public IReadOnlyDictionary<AudioSourceType, AudioCell> Data => data;
    }

    [Serializable]
    public class AudioCell
    {
        [field: SerializeField] public AudioSourceHolderPoolable AudioSourceHolderPoolablePrefab { get; private set; }
        [field: SerializeField] public float Pitch { get; private set; }
        [field: SerializeField] public float PitchRange { get; private set; }
    }
}