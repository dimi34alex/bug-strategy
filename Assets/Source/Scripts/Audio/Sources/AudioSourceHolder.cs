using UnityEngine;

namespace BugStrategy.Audio.Sources
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHolder : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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
    }
}