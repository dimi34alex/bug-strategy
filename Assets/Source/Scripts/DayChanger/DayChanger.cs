using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayChanger : MonoBehaviour
{
    [SerializeField] private float _cycleDuration;
    [SerializeField] private PostProcessVolume _volume;
    [SerializeField] private AnimationCurve _dayCurve;

    private const float MAX_VIGNETTE_INTENSITY = 0.6F;
    private const float MIN_POST_EXPOSURE = -1.8F;

    private Vignette _vignette;
    private ColorGrading _colorGrading;

    private float _currentDayTime = 0.5f; // [0; 1)

    public DayChanger Instance { get; private set; }

    private void OnValidate()
    {
        if (!_volume)
            _volume = GetComponent<PostProcessVolume>();
    }

    private void Awake()
    {
        Instance = this;
        _vignette = _volume.profile.GetSetting<Vignette>();
        _colorGrading = _volume.profile.GetSetting<ColorGrading>();
    }

    private void Update()
    {
        IncreaseDayTime();

        float nightIntensity = (Mathf.Abs(1f - _dayCurve.Evaluate(_currentDayTime)));

        _vignette.intensity.Override(nightIntensity * MAX_VIGNETTE_INTENSITY);
        _colorGrading.postExposure.Override(nightIntensity * MIN_POST_EXPOSURE);
    }

    private void IncreaseDayTime()
    {
        _currentDayTime = Mathf.Sin((Time.time * (Mathf.PI * 2f)) / (_cycleDuration * 2f));
        _currentDayTime = _currentDayTime / 2f + 0.5f;
    }

    public TimeSpan GetTime()
    {
        float remainingTime = _currentDayTime;

        int hours = (int)(remainingTime * 24f);
        remainingTime -= hours;

        int minutes = (int)(remainingTime * 60f);
        remainingTime -= minutes;

        int seconds = (int)(remainingTime * 60f);

        return new TimeSpan(hours, minutes, seconds);
    }
}
