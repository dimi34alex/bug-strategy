using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SelectableSystem
{
    [Serializable]
    public class AbilitiesBar : MonoBehaviour
    {
        [SerializeField] private GameObject abilityBlockPrefab;
        [SerializeField] private Transform firstIconTransform;
        [SerializeField] private float distanceBetweenIcons;

        private List<AbilityBlock> _abilityBlocks = new List<AbilityBlock>();

        public void Init(IReadOnlyList<AbilityBase> abilities)
        {
            Vector3 firstIconPosition = firstIconTransform.position;
            for (int n = 0; n < abilities.Count; n++)
            {
                GameObject abilityBlockObj = UnityEngine.Object.Instantiate(abilityBlockPrefab,
                    new Vector3(firstIconPosition.x + distanceBetweenIcons * n, firstIconPosition.y,
                        firstIconPosition.z),
                    firstIconTransform.rotation, firstIconTransform);

                AbilityBlock abilityBlock = abilityBlockObj.GetComponent<AbilityBlock>();
                abilityBlock.SetData(abilities[n].AbilityIcon, abilities[n]);
                _abilityBlocks.Add(abilityBlock);
            }
        }

        public void OnSelect()
        {
            for (int i = 0; i < _abilityBlocks.Count; i++)
                _abilityBlocks[i].Subscribe();
        }

        public void OnDeselect()
        {
            for (int i = 0; i < _abilityBlocks.Count; i++)
                _abilityBlocks[i].UnSubscribe();
        }
    }
}