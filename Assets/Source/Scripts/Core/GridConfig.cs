using System;
using BugStrategy.ConfigsRepository;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy
{
    [CreateAssetMenu(fileName = nameof(GridConfig), menuName = "Configs/" + nameof(GridConfig))]
    public class GridConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private Vector2 _hexagonsOffcets;

        public Vector2 HexagonsOffsets => _hexagonsOffcets;
        
        public Vector3 RoundPositionToGrid(Vector3 position)
        {
            var xPosition = position.x.Round(HexagonsOffsets.x / 2);

            var horizontalLineIndex = Convert.ToInt32(xPosition / (HexagonsOffsets.x / 2f));

            var zPosition = (position.z - HexagonsOffsets.y / 2f).Round(HexagonsOffsets.y);

            var verticalLineIndex = Convert.ToInt32(zPosition / HexagonsOffsets.y);

            if (horizontalLineIndex % 2 == 0 && verticalLineIndex % 2 != 0 || horizontalLineIndex % 2 != 0 && verticalLineIndex % 2 == 0)
                zPosition += HexagonsOffsets.y;

            return new Vector3(xPosition, 0f, zPosition);
        }
    }
}

