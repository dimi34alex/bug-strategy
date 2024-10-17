using BugStrategy.Factory;
using BugStrategy.MiniMap.MiniMapIcons.Configs;
using Zenject;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    // public class MiniMapIconFactory : MonoBehaviour, IMiniMapIconFactory
    // {
    //     private IReadOnlyDictionary<MiniMapIconID, IMiniMapIconFactoryBehaviour> _behaviours;
    //
    //     private void Awake()
    //     {
    //         _behaviours = GetComponentsInChildren<IMiniMapIconFactoryBehaviour>(true)
    //             .ToDictionary(behaviour => behaviour.MiniMapIconID, behaviour => behaviour);
    //
    //         foreach (IMiniMapIconFactoryBehaviour behaviour in _behaviours.Values)
    //             Debug.Log($"Factory behaviour {behaviour.GetType()} has been registered");
    //     }
    //
    //     public TMiniMapIcon Create<TMiniMapIcon>(MiniMapIconID miniMapIconID) where TMiniMapIcon : MiniMapIconBase
    //     {
    //         if (!_behaviours.ContainsKey(miniMapIconID))
    //             throw new InvalidOperationException($"{miniMapIconID} cannot be created, " +
    //                                                 $"because factory for this mini map icon not found. Create new factory behaviour for this mini map icon");
    //
    //         return _behaviours[miniMapIconID].Create<TMiniMapIcon>();
    //     }
    // }
    
    public class MiniMapIconFactory : ObjectsFactoryBase<MiniMapIconID, MiniMapIconBase>
    {
        public MiniMapIconFactory(DiContainer diContainer, MiniMapIconsConfig config) : 
            base(diContainer, config, "MiniMapIcons") { }
    }
}