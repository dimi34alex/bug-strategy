using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BuildProgressConstructions
{
	[CreateAssetMenu(fileName = nameof(BuildingProgressConstructionSpawnHealthConfig), menuName = "Configs/Constructions/Main/" + nameof(BuildingProgressConstructionSpawnHealthConfig))]
	public class BuildingProgressConstructionSpawnHealthConfig : ScriptableObject, ISingleConfig
	{
		[SerializeField][Min(0)] public int MaxHealthPoints;
	}
}