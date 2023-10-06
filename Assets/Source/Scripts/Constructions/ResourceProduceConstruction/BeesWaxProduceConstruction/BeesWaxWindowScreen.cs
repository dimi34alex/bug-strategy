using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeesWaxWindowScreen : UIScreen
{
    [SerializeField] private Slider _selectProduceSlider;
    [SerializeField] private TMP_Text _selectResourceCountText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _receiveButton;
    [SerializeField] private TMP_Text _producedResourceCountText;
}