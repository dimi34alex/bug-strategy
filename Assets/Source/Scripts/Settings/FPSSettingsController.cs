using TMPro;
using UnityEngine;

namespace BugStrategy.Settings
{
    public class FPSSettingsController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown fpsDropdown;

        private readonly int[] _fpsOptions = { 30, 60, 90, 120, 144, 240, 360 };

        private void Start()
        {
            fpsDropdown.ClearOptions();
            foreach (var fps in _fpsOptions) 
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(fps + " FPS"));

            var maxFpsIndex = _fpsOptions.Length - 1;
            fpsDropdown.value = maxFpsIndex;
            fpsDropdown.RefreshShownValue();
        }

        public void Apply()
        {
            var selectedFps = _fpsOptions[fpsDropdown.value];
            Application.targetFrameRate = selectedFps;
        }

        public void ResetToDefault()
        {
            fpsDropdown.value = _fpsOptions.Length - 1;
            Application.targetFrameRate = _fpsOptions[fpsDropdown.value];
        }
    }
}