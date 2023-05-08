using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniMapTriggerData : TriggerZone
{
    [Serializable]
    private struct CameraMiniMapDictionaryData
    {
        public MiniMapID iconId;
        public GameObject iconPrefab;
    }
    
    [SerializeField] private List<CameraMiniMapDictionaryData> iconsPrefabsData;
    private Dictionary<MiniMapID, GameObject> _iconsPrefabs;
    
    private Pool<MiniMapIconBase, MiniMapID> _miniMapIcons;
    private GameObject _miniMapIconZone;

    public Dictionary<IMiniMapShows, MiniMapIconBase> MiniMapIcons { get; private set;}

    public static MiniMapTriggerData Instance;

    private Func<IMiniMapShows, bool> _filter = t => true;
    protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is IUnitTarget && _filter(t.Cast<IMiniMapShows>());
    protected override bool _refreshEnteredComponentsAfterExit => false;

    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        _iconsPrefabs = new Dictionary<MiniMapID, GameObject>();
        foreach (var iconPrefab in iconsPrefabsData)
        {
            _iconsPrefabs.Add(iconPrefab.iconId, iconPrefab.iconPrefab);
        }
        
        _miniMapIcons = new Pool<MiniMapIconBase, MiniMapID>(AddIcon);
        MiniMapIcons = new Dictionary<IMiniMapShows, MiniMapIconBase>();
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

    protected override void OnEnter(ITriggerable component)
    {
        IMiniMapShows miniMapShows = component.Cast<IMiniMapShows>();

        MiniMapID id = miniMapShows.MiniMapId;
        
        MiniMapIconBase icon = _miniMapIcons.ExtractElement(id);
        icon.gameObject.SetActive(true);
        
        Vector3 iconPosition = miniMapShows.Transform.position;
        iconPosition.y = 0;
        icon.transform.localPosition = iconPosition;
        MiniMapIcons.Add(miniMapShows, icon);  
    }

    protected override void OnExit(ITriggerable component)
    {
        IMiniMapShows miniMapShows = component.Cast<IMiniMapShows>();
        
        if(MiniMapIcons.ContainsKey(miniMapShows))
            IconRemove(miniMapShows);
    }

    private void IconRemove(IMiniMapShows transformForRemove)
    {
        MiniMapIcons.TryGetValue(transformForRemove, out MiniMapIconBase icon);

        if(icon == null) return;
        
        icon.Return();
        icon.gameObject.SetActive(false);
        MiniMapIcons.Remove(transformForRemove);
    }
}