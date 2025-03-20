using System;
using BugStrategy.CustomTimer;
using BugStrategy.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BugStrategy.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolderPoolable : MonoBehaviour, IPoolable<AudioSourceHolderPoolable>, IPoolEventListener
    {
        [SerializeField] private float minPitch = 1f;
        [SerializeField] private float maxPitch = 1f;

        private AudioSource _audioSource;
        private Timer _existTimer;
        
        public event Action<AudioSourceHolderPoolable> ElementReturnEvent;
        public event Action<AudioSourceHolderPoolable> ElementDestroyEvent;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            _existTimer = new Timer(_audioSource.clip.length);
            _existTimer.OnTimerEnd += () => ElementReturnEvent?.Invoke(this);
        }

        public void AutoPitch() 
            => _audioSource.pitch = Random.Range(minPitch, maxPitch);

        private void Update() 
            => _existTimer.Tick(Time.deltaTime);

        public void SetPitch(float newPitch) 
            => _audioSource.pitch = newPitch;

        public void OnElementExtract()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif

            _existTimer.Reset();
            _audioSource.Play();
        }

        public void OnElementReturn()
        {
            _existTimer.SetPause();
            _audioSource.Stop();
            
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        private void OnDestroy() 
            => ElementDestroyEvent?.Invoke(this);
    }
}