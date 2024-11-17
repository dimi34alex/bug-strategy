using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static BugStrategy.SettingsController;
using System;
using UnityEngine.SceneManagement;
using BugStrategy.Audio;
using Zenject;

namespace BugStrategy
{
    public class SettingsController : MonoBehaviour
    {
        [Inject] private readonly AudioVolumeChanger _audioVolumeChanger;

        [SerializeField] private TMP_Dropdown screenModeDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown fpsDropdown;

        private Resolution[] resolutions;
        private int[] fpsOptions = { 75, 100, 144, 165, 360 };

        void Start()
        {
            QualitySettings.vSyncCount = 0;

            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                Resolution res = resolutions[i];
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));

                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            fpsDropdown.ClearOptions();
            foreach (var fps in fpsOptions)
            {
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(fps + " FPS"));
            }

            int maxFpsIndex = fpsOptions.Length - 1;
            fpsDropdown.value = maxFpsIndex;
            fpsDropdown.RefreshShownValue();

            screenModeDropdown.value = Screen.fullScreen ? 1 : 0;
        }

        public void ApplySettings()
        {
            Resolution selectedResolution = resolutions[resolutionDropdown.value];
            bool isFullScreen = screenModeDropdown.value == 1; // 0 - оконный, 1 - полный экран

            Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);

            int selectedFps = fpsOptions[fpsDropdown.value];
            Application.targetFrameRate = selectedFps;

            _audioVolumeChanger.Apply();
        }
    }
}
