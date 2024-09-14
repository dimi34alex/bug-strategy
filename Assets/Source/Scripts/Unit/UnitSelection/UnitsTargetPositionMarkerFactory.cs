using BugStrategy.Pool;
using UnityEngine;

namespace BugStrategy.Unit.UnitSelection
{
	public class UnitsTargetPositionMarkerFactory : MonoBehaviour
	{
		[SerializeField] private UnitsTargetPositionMarker _markerPrefab;

		public static UnitsTargetPositionMarkerFactory Instance { get; private set; }

		private Pool<UnitsTargetPositionMarker> _markersPool;

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(this);
				return;
			}

			Instance = this;

			_markersPool = new Pool<UnitsTargetPositionMarker>(CreateNewMarker, 3, false);
		}

		private UnitsTargetPositionMarker CreateNewMarker()
		{
			UnitsTargetPositionMarker targetPositionMarker =
				Instantiate<UnitsTargetPositionMarker>(_markerPrefab, transform);

			return targetPositionMarker;
		}

		public UnitsTargetPositionMarker Create(Vector2 targetPosition)
		{
			UnitsTargetPositionMarker marker = _markersPool.ExtractElement();

			marker.SetPosition(targetPosition);

			return marker;
		}
	}
}
