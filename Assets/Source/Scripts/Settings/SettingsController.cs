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
        // Dropdowns для настроек экрана
        public TMP_Dropdown screenModeDropdown;
        public TMP_Dropdown resolutionDropdown;
        public TMP_Dropdown fpsDropdown;

        // Массивы разрешений и FPS
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

                // Проверяем, совпадает ли разрешение с текущим
                if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            // Устанавливаем текущее разрешение
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            // Добавляем варианты FPS
            fpsDropdown.ClearOptions();
            foreach (var fps in fpsOptions)
            {
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(fps + " FPS"));
            }

            // Устанавливаем текущее значение для режима экрана (0 - окно, 1 - полный экран)
            screenModeDropdown.value = Screen.fullScreen ? 1 : 0;
        }

        public void ApplySettings()
        {
            // Применяем разрешение и режим экрана
            Resolution selectedResolution = resolutions[resolutionDropdown.value];
            bool isFullScreen = screenModeDropdown.value == 1; // 0 - оконный, 1 - полный экран

            Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);

            // Применяем FPS
            int selectedFps = fpsOptions[fpsDropdown.value];
            Application.targetFrameRate = selectedFps;
        }
    }
}
