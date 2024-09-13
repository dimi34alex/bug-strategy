using System;
using Source.Scripts.Missions;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class ConstructionTester : CycleInitializerBase
{
    [Inject] private readonly IConstructionFactory _constructionFactory;

    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            int index = raycastHits.IndexOf(hit => !hit.collider.isTrigger);

            if (index > -1)
            {
                Vector3 position = GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.RoundPositionToGrid(raycastHits[index].point);

                if (GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.ConstructionExist(position.ToInt(), false))
                    return;

                BuildingProgressConstruction progressConstruction = 
                    _constructionFactory.Create<BuildingProgressConstruction>(ConstructionID.BuildingProgressConstruction, AffiliationEnum.None);
                progressConstruction.transform.position = position;
                GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.AddConstruction(position.ToInt(), progressConstruction);

                progressConstruction.OnTimerEnd += c => CreateDefaultConstruction(c, position.ToInt());
                //progressConstruction.StartBuilding(4, ConstructionID.Test_Construction);
            }
        }
    }

    private void CreateDefaultConstruction(BuildingProgressConstruction buildingProgressConstruction, Vector3Int position)
    {
        DefaultConstruction defaultConstruction =
            _constructionFactory.Create<DefaultConstruction>(buildingProgressConstruction.BuildingConstructionID, AffiliationEnum.None);

        GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository
            .GetConstruction(position, true);

        Destroy(buildingProgressConstruction.gameObject);

        GlobalDataHolder.GlobalData.ActiveMission.ConstructionsRepository.AddConstruction(position, defaultConstruction);
        defaultConstruction.transform.position = position;
    }
}
