using Constructions;
using Source.Scripts.Missions;
using UnityEngine;
using Zenject;

public class BeesWaxProduceConstructionTester : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;

    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonUp(2))
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

            if (index > -1)
            {
                Vector3 position = GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

                if (GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.ConstructionExist(position.ToInt(), false))
                    return;

                BeesWaxProduceConstruction construction =
                    _constructionFactory.Create<BeesWaxProduceConstruction>(ConstructionID.BeeWaxProduceConstruction, AffiliationEnum.None);
                construction.transform.position = position;
                GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.AddConstruction(position.ToInt(), construction);
            }
        }
    }
}
