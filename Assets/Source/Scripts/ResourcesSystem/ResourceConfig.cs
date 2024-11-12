using UnityEngine;

namespace BugStrategy.ResourcesSystem
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/Resources/ResourceConfig")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private ResourceID _id;
        [SerializeField] private Sprite _icon;

        public ResourceID ID => _id;
        public Sprite Icon => _icon;
    }
}