using System;
using System.Collections.Generic;
using Source.Scripts.Unit.AbilitiesCore;
using UnityEngine;

namespace SelectableSystem
{
    [Serializable]
    public class AbilitiesBar : MonoBehaviour, OnSelectionUI
    {
        [SerializeField] private GameObject abilityBlockPrefab;
        [SerializeField] private Transform firstIconTransform;
        [SerializeField] private float distanceBetweenIcons;

        private List<AbilityBlock> _abilityBlocks = new();

        public void Init(IReadOnlyList<IAbility> abilities, AbilitiesUiConfig abilitiesUiConfig)
        {
            var firstIconPosition = firstIconTransform.position;
            for (int n = 0; n < abilities.Count; n++)
            {
                var abilityBlockObj = Instantiate(abilityBlockPrefab,
                    new Vector3(firstIconPosition.x + distanceBetweenIcons * n, firstIconPosition.y,
                        firstIconPosition.z), firstIconTransform.rotation, firstIconTransform);

                var abilityBlock = abilityBlockObj.GetComponent<AbilityBlock>();
                abilityBlock.SetData(abilitiesUiConfig.AbilitiesUiIcons[abilities[n].AbilityType], abilities[n]);
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