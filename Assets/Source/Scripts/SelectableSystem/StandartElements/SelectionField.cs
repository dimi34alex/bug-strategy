using UnityEngine;

namespace SelectableSystem
{
    public class SelectionField : MonoBehaviour
    {
        public void Init(bool isSelected) => gameObject.SetActive(isSelected);

        public void OnSelect() => gameObject.SetActive(true);

        public void OnDeselect() => gameObject.SetActive(false);
    }
}