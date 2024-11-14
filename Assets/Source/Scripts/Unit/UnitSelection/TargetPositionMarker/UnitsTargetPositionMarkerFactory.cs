using BugStrategy.Factories;
using Zenject;

namespace BugStrategy.Unit.UnitSelection.TargetPositionMarker
{
	public class UnitsTargetPositionMarkerFactory : FactorySinglePool<UnitsTargetPositionMarker>
	{
		protected UnitsTargetPositionMarkerFactory(DiContainer diContainer, UnitsTargetPositionMarkerConfig config) 
			: base(diContainer, config.Prefab, "UnitsTargetPositionMarkers", config.MaxMarkersCountPerTime ,false) { }
	}
}
