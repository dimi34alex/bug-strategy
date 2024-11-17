using System;
using BugStrategy.CustomTimer;
using BugStrategy.Pool;
using UnityEngine;

namespace BugStrategy.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolderPoolable : MonoBehaviour, IPoolable<AudioSourceHolderPoolable, AudioSourceType>, IPoolEventListener
    {
        [field: SerializeField] public AudioSourceType PoolId { get; private set; }
        [SerializeField] private bool useLocalPause = true;
        
        public AudioSourceType Identifier => PoolId;

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

        private void Update() 
            => _existTimer.Tick(useLocalPause ? Time.deltaTime : Time.unscaledDeltaTime);

        public void SetPitch(float newPitch) 
            => _audioSource.pitch = newPitch;

        public void OnElementExtract()
        {
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif

            if (useLocalPause)
            {
                // LocalGamePause.OnPaused += Pause;
                // LocalGamePause.OnContinued += Continue;
            }

            _existTimer.Reset();
            _audioSource.Play();
        }

        public void OnElementReturn()
        {
            _existTimer.SetPause();
            _audioSource.Stop();
            
            // LocalGamePause.OnPaused -= Pause;
            // LocalGamePause.OnContinued -= Continue;
            
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        private void Pause() 
            => _audioSource.Pause();

        private void Continue() 
            => _audioSource.Play();

        private void OnDestroy() 
            => ElementDestroyEvent?.Invoke(this);
    }
}