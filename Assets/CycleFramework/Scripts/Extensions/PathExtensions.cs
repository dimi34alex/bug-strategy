using UnityEngine;

namespace CycleFramework.Extensions
{
    public static class PathExtensions
    {
        public static Vector3[] CreateFlyPath(this Vector3 startPosition, Vector3 elementFinalPosition, float height)
        {
            return new Vector3[]
            {
                (startPosition + elementFinalPosition) / 2f + Vector3.up * height,
                elementFinalPosition
            };
        }

        public static Vector3[] CreateFlyPath(this Transform transform, Vector3 elementFinalPosition, float height) =>
            CreateFlyPath(transform.position, elementFinalPosition, height);

        public static Vector3[] CreateStaticPath(this Transform pathParent)
        {
            Vector3[] path = new Vector3[pathParent.childCount];

            for (int i = 0; i < path.Length; i++)
                path[i] = pathParent.GetChild(i).position;

            return path;
        }
    }
}