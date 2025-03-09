using UnityEngine;

namespace BugStrategy.CameraMovement
{
    [CreateAssetMenu(fileName = nameof(CameraMoveConfig), menuName = "Configs/Camera/" + nameof(CameraMoveConfig))]
    public class CameraMoveConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeedByWheel { get; private set; } = 10;
        [field: SerializeField] public float MoveSpeedByCursor { get; private set; } = 25;
        [field: SerializeField] public float PercentageOfScreen { get; private set; } = 2;
    }
}