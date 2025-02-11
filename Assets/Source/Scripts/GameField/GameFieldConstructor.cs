using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.GameField
{
    public class GameFieldConstructor : MonoBehaviour
    {
        [SerializeField] private GameObject physicField;
        [Header("Walls")]
        [SerializeField] private GameObject upWall;
        [SerializeField] private GameObject downWall;
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;
        
        public void SetFieldSize(Vector2 size)
        {
            physicField.transform.localScale = size.XoY(1);

            upWall.transform.position = new Vector3(0, 0, size.y);
            downWall.transform.position = new Vector3(0, 0, -size.y);
            leftWall.transform.position = new Vector3(-size.x, 0, 0);
            rightWall.transform.position = new Vector3(size.x, 0, 0);

            upWall.transform.localScale = new Vector3(size.x,1,1) * 2;
            downWall.transform.localScale = new Vector3(size.x,1,1) * 2;
            leftWall.transform.localScale = new Vector3(1,1,size.y) * 2;
            rightWall.transform.localScale = new Vector3(1,1,size.y) * 2;
        }
    }
}