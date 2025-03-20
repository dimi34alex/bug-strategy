using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Unit.UnitSelection
{
    public static class RingStepPositionGenerator
    {
        private static float _ringStep = 2;
        private static int _unitsCountRingStep = 8;

        public static List<Vector3> TakeRingsPositions(Vector3 center, int countPositions)
        {
            float currentDistance = _ringStep;
            int currentCount = _unitsCountRingStep;

            List<Vector3> positions = new List<Vector3>();
            positions.Add(center);
            for (int i = positions.Count; i < countPositions; i = positions.Count)
            {
                positions.AddRange(TakeRingPositions(center, currentDistance, currentCount));
                currentDistance += _ringStep;
                currentCount += _unitsCountRingStep;
            }

            return positions;
        }

        private static List<Vector3> TakeRingPositions(Vector3 center, float distanceFromCenter, int positionsCount)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < positionsCount; i++)
            {
                float angle = i * (360 / positionsCount);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * new Vector3(1, 0, 0);
                positions.Add(center + distanceFromCenter * direction);
            }

            return positions;
        }
    }
}