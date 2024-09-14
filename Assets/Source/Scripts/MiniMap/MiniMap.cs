using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.MiniMap.MiniMapIcons.Factories;
using BugStrategy.Pool;
using UnityEngine;
using Zenject;

namespace BugStrategy.MiniMap
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField] private GameObject miniMapIconParent;
        [SerializeField] private float iconsScale;

        [Inject] private readonly IMiniMapIconFactory _miniMapIconFactory; 
        [Inject] private readonly IMiniMapTriggerZone _miniMapTriggerZone;
    
        private Vector3 _localIconsScale;
        private Transform _cameraTransform;
        private Pool<MiniMapIconBase, MiniMapIconID> _miniMapIconsPool;
        private readonly Dictionary<MiniMapIconID, List<MiniMapIconBase>> _miniMapIcons = new Dictionary<MiniMapIconID, List<MiniMapIconBase>>();
        private Dictionary<MiniMapIconID, IReadOnlyList<IMiniMapObject>> _miniMapObjects;
        
        private void Awake()
        {
            _miniMapIconsPool = new Pool<MiniMapIconBase, MiniMapIconID>(InstantiateIcon);
            
            foreach (var miniMapIconID in Enumerable.Cast<MiniMapIconID>(Enum.GetValues(typeof(MiniMapIconID))))
                _miniMapIcons.Add(miniMapIconID, new List<MiniMapIconBase>());
        }

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            _miniMapObjects = _miniMapTriggerZone.MiniMapObjects;
            _miniMapTriggerZone.OnObjectAdd += AddIcons;
            _miniMapTriggerZone.OnObjectRemove += RemoveIcon;
            
            miniMapIconParent.transform.localScale = new Vector3(120 / _miniMapTriggerZone.Scale.x, 1, 120 / _miniMapTriggerZone.Scale.y);
            _localIconsScale = new Vector3(_miniMapTriggerZone.Scale.x / 120, _miniMapTriggerZone.Scale.y / 120, 1) * iconsScale;
        }

        private void Update() => UpdateIcons();

        private void UpdateIcons()
        {
            Vector3 cameraPos = _cameraTransform.position;
            cameraPos.y = 0;
            
            foreach (var key in _miniMapObjects.Keys)
            {
                for (int i = 0; i < _miniMapObjects[key].Count; i++)
                {
                    Vector3 objectPosition = _miniMapObjects[key][i].Transform.position;
                    objectPosition.y = 0;
                    
                    _miniMapIcons[key][i].transform.localPosition = objectPosition - cameraPos;
                }
            }
        }
        
        private void AddIcons(MiniMapIconID miniMapIconID)
        {
            var icon = _miniMapIconsPool.ExtractElement(miniMapIconID);
            icon.ElementDestroyEvent += OnIconDestroy;
            icon.gameObject.SetActive(true);
            _miniMapIcons[miniMapIconID].Add(icon);
        }

        private void RemoveIcon(MiniMapIconID miniMapIconID)
        {
            if(_miniMapIcons[miniMapIconID].Count <= 0) return;

            MiniMapIconBase icon = _miniMapIcons[miniMapIconID][_miniMapIcons[miniMapIconID].Count-1];
            icon.Return();
            icon.gameObject.SetActive(false);
            _miniMapIcons[miniMapIconID].RemoveAt(_miniMapIcons[miniMapIconID].Count-1); 
        }

        private MiniMapIconBase InstantiateIcon(MiniMapIconID miniMapIconID)
        {
            MiniMapIconBase icon = _miniMapIconFactory.Create<MiniMapIconBase>(miniMapIconID);
            icon.transform.SetParent(miniMapIconParent.transform, false);
            icon.transform.localScale = _localIconsScale;

            return icon;
        }

        private void OnIconDestroy(MiniMapIconBase icon)
        {
            _miniMapIcons[icon.Identifier].Remove(icon);
        }
    }
}