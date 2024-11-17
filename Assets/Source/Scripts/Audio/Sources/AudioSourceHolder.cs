using UnityEngine;

namespace BugStrategy.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolder : MonoBehaviour
    {
        [SerializeField] private bool useLocalPause = true;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            // LocalGamePause.OnPaused += Pause;
            // LocalGamePause.OnContinued += Continue;
        }

        private void Play()
        {
            _audioSource.Stop();
            _audioSource.Play();
        }

        public void SetPitch(float newPitch) 
            => _audioSource.pitch = newPitch;

        private void Pause() 
            => _audioSource.Pause();

        private void Continue() 
            => _audioSource.Play();

        private void OnDestroy()
        {
            // LocalGamePause.OnPaused -= Pause;
            // LocalGamePause.OnContinued -= Continue;
        }
    }
}