using BugStrategy.ResourcesSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
	public sealed class LeavesStorage : ResourceSourceBase
	{
		[SerializeField] private int refillTime = 30;
		[SerializeField] private Sprite defaultSkin;
		[SerializeField] private Sprite pollinatedSkin;

		public override ResourceID ResourceID => ResourceID.Leaves;

		public override void ExtractResource(int extracted)
		{
			ResourceStorage.ChangeValue(-extracted);

			if (ResourceStorage.CurrentValue <= 0)
			{
				CanBeCollected = false;
				View.SetView(pollinatedSkin);
				StartCoroutine(StartRePollinating(refillTime));
			}
		}

		private IEnumerator StartRePollinating(int duration)
		{
			yield return new WaitForSeconds(duration);

			CanBeCollected = true;
			View.SetView(defaultSkin);
			ResourceStorage.SetValue(ResourceStorage.Capacity);
		}
	}
}
