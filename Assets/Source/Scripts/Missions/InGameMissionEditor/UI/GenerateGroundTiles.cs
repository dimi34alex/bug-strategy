using BugStrategy.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    public class GenerateGroundTiles : MonoBehaviour
    {
        [SerializeField] private TMP_NumberInputField xSize;
        [SerializeField] private TMP_NumberInputField ySize;
        [SerializeField] private Button applyButton;

        [SerializeField] private MissionEditorBuilder missionEditorBuilder;
        
        private void Awake()
        {
            applyButton.onClick.AddListener(Generate);
        }

        private void Generate()
        {
            if (string.IsNullOrEmpty(xSize.text) || string.IsNullOrEmpty(ySize.text))
                return;

            var size = new Vector2Int(int.Parse(xSize.text), int.Parse(ySize.text));
            missionEditorBuilder.Generate(size);
        }
    }
}