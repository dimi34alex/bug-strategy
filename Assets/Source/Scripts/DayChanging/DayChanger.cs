using CycleFramework.Execute;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace BugStrategy.DayChanging
{
    public class DayChanger : CycleInitializerBase
    {
        [SerializeField] private float _cycleDuration;
        [SerializeField, Range(0,24)] private float _initialTime;
        [SerializeField] private PostProcessVolume _volume;
        [SerializeField] private AnimationCurve _dayCurve;
        [SerializeField] private float _maxVignetteIntensity = 0.6F;
        [SerializeField] private float _minPostExposure = -1.8F;

        private Vignette _vignette;
        private ColorGrading _colorGrading;
        private float _gameTimer;
        private float _currentDayTime; // [0; 24)

        private void Awake()
        {
            _vignette = _volume.profile.GetSetting<Vignette>();
            _colorGrading = _volume.profile.GetSetting<ColorGrading>();

            _gameTimer = _cycleDuration * _initialTime/24;
        }

        protected override void OnStartInit()
        {
            CalculateDayTime();
            ChangeNightIntensity();
        }

        protected override void OnUpdate()
        {
            _gameTimer += Time.deltaTime;

            CalculateDayTime();
            ChangeNightIntensity();
        }
        
        public string GetTime()
        {
            var hours = (int)_currentDayTime;
            var minutes = (int)((_currentDayTime - hours) * 60f);

            return $"{hours:00}:{minutes:00}";
        }

        private void ChangeNightIntensity()
        {
            var nightIntensity = Mathf.Abs(1f - _dayCurve.Evaluate(_currentDayTime));

            _vignette.intensity.Override(nightIntensity * _maxVignetteIntensity);
            _colorGrading.postExposure.Override(nightIntensity * _minPostExposure);
        }
        
        private void CalculateDayTime()
        {
            _currentDayTime = 24 * _gameTimer / _cycleDuration;
            _currentDayTime %= 24;
        }
    }
}
