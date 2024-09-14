using System.Collections;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
    public sealed class PollenStorage : ResourceSourceBase
    {
        [SerializeField] private int refillTime = 30;
        [SerializeField] private GameObject model;
        [SerializeField] private GameObject pollinatedModel;
    
        public override ResourceID ResourceID => ResourceID.Pollen;

        public override void ExtractResource(int extracted)
        {
            ResourceStorage.ChangeValue(-extracted);

            if (ResourceStorage.CurrentValue <= 0)
            {
                CanBeCollected = false;
                model.SetActive(false);
                pollinatedModel.SetActive(true);
                StartCoroutine(StartRePollinating(refillTime));
            }
        }
    
        private IEnumerator StartRePollinating(int duration)
        {
            yield return new WaitForSeconds(duration);

            CanBeCollected = true;
            pollinatedModel.SetActive(false);
            ResourceStorage.SetValue(float.MaxValue);
            model.SetActive(true);
        }
    }
}
