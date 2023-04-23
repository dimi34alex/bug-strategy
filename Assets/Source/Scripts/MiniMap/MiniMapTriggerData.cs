using System;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTriggerData : MonoBehaviour
{
    [Serializable]
    private struct CameraMiniMapDictionaryData
    {
        public MiniMapID iconId;
        public GameObject iconPrefab;
    }
    [SerializeField] private List<CameraMiniMapDictionaryData> dictionaryData;
    private Dictionary<MiniMapID, GameObject> _iconsPrefabs;
    
    private Pool<MiniMapIconBase, MiniMapID> _miniMapIcons;
    private GameObject _miniMapIconZone;

    public Dictionary<Transform, MiniMapIconBase> MiniMapIcons { get; private set;}

    public static MiniMapTriggerData Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        
        _iconsPrefabs = new Dictionary<MiniMapID, GameObject>();
        foreach (var iconPrefab in dictionaryData)
        {
            _iconsPrefabs.Add(iconPrefab.iconId, iconPrefab.iconPrefab);
        }
        
        _miniMapIcons = new Pool<MiniMapIconBase, MiniMapID>(AddIcon);
        MiniMapIcons = new Dictionary<Transform, MiniMapIconBase>();
    }
    
    private void Start()
    {
        _miniMapIconZone = UIScreenRepository.GetScreen<UI_GameplayMain>().MiniMapIconZone;
        
        float triggerColliderRadius = GetComponent<SphereCollider>().radius;
        float miniMapIconZoneScale = triggerColliderRadius / 25;
        miniMapIconZoneScale = 4 / miniMapIconZoneScale;
        _miniMapIconZone.transform.localScale =
            new Vector3(miniMapIconZoneScale, miniMapIconZoneScale, miniMapIconZoneScale);
    }

    private MiniMapIconBase AddIcon(MiniMapID id)
    {
        GameObject iconPrefab;

        _iconsPrefabs.TryGetValue(id, out iconPrefab);

        if (iconPrefab == null)
            throw new Exception("MiniMapID dont found in dictionary");
        
        GameObject icon = Instantiate(iconPrefab, _miniMapIconZone.transform.position, 
            new Quaternion(-90, 0, 0, 0), _miniMapIconZone.transform);
        
        return icon.GetComponent<MiniMapIconBase?>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IMiniMapShows miniMapShows = other.GetComponent<IMiniMapShows?>();

        if (miniMapShows == null)
            throw new Exception("IMiniMapObject is null");

        if (MiniMapIcons.ContainsKey(other.transform))
            return;

        miniMapShows.RemoveMiniMapIcon += IconRemove;
        
        MiniMapID id = miniMapShows.MiniMapId;
        
        MiniMapIconBase icon = _miniMapIcons.ExtractElement(id);
        icon.gameObject.SetActive(true);
        
        Vector3 iconPosition = other.transform.position;
        iconPosition.y = 0;
        icon.transform.localPosition = iconPosition;
        MiniMapIcons.Add(other.transform, icon);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(MiniMapIcons.ContainsKey(other.transform))
            IconRemove(other.transform);
    }

    private void IconRemove(Transform transformForRemove)
    {
        MiniMapIconBase icon;
        MiniMapIcons.TryGetValue(transformForRemove, out icon);

        if(icon == null) return;
        
        icon.Return();
        transformForRemove.GetComponent<IMiniMapShows>().RemoveMiniMapIcon -= IconRemove;
        
        icon.gameObject.SetActive(false);
        MiniMapIcons.Remove(transformForRemove);
    }
}