using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static BugStrategy.SettingsController;
using System;
using UnityEngine.SceneManagement;

namespace BugStrategy
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown screenModeDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown fpsDropdown;

        private Resolution[] resolutions;
        private int[] fpsOptions = { 30, 60, 120, 240 };

        void Start()
        {
            QualitySettings.vSyncCount = 0;

            // Инициализация разрешений и очистка вариантов в dropdown
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;

            // Добавляем варианты разрешений в dropdown
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

            //fpsDropdown.ClearOptions();
            foreach (var fps in fpsOptions)
            {
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(fps + " FPS"));
            }

            screenModeDropdown.value = Screen.fullScreen ? 1 : 0;
        }

        public void ApplySettings()
        {
            Resolution selectedResolution = resolutions[resolutionDropdown.value];
            bool isFullScreen = screenModeDropdown.value == 1; // 0 - оконный, 1 - полный экран

            Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);

            int selectedFps = fpsOptions[fpsDropdown.value];
            Application.targetFrameRate = selectedFps;
        }
    }
}
