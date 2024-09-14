using UnityEngine;

namespace BugStrategy.Constructions.AntHouse
{
    [CreateAssetMenu(fileName = nameof(AntHouseConfig), menuName = "Configs/Constructions/Main/" + nameof(AntHouseConfig))]
    public class AntHouseConfig : EvolveConstructionConfigBase<AntHouseLevel>
    {
        
    }
}