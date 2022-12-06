using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BarracksMenu : UIScreen
{
    [SerializeField] private UI_ERROR uiError;

    [SerializeField] private List<TextMeshProUGUI> StackID;
    [SerializeField] private List<TextMeshProUGUI> StackTime;
    Barrack barrack;
    int currentStack = 0;
    BeeRecruitingInformation beeRecruitingInformation;

    private void Update()
    {

        beeRecruitingInformation = barrack._GetBeeRecruitingInformation(currentStack);

        if (beeRecruitingInformation.Empty)
        {
            StackID[currentStack].text = "empty";
            StackTime[currentStack].text = "";
        }
        else{
            StackID[currentStack].text = beeRecruitingInformation.CurrentID.ToString();
            StackTime[currentStack].text = (Mathf.Clamp((Mathf.Round(beeRecruitingInformation.CurrentTime * 100F) / 100F), 0F, Mathf.Infinity).ToString() + "/" + (Mathf.Round(beeRecruitingInformation.RecruitinTime * 100F) / 100F).ToString());
        }

        currentStack++;
        if (currentStack >= barrack.RecruitingSize || currentStack >= StackID.Count || currentStack >= StackTime.Count)
            currentStack = 0;
    }

    public void _RecruitingWax()
    {
        uiError._ErrorCall(barrack._RecruitBees(BeesRecruitingID.Wasp));
    }
    public void _RecruitingBumblebee()
    {
        uiError._ErrorCall(barrack._RecruitBees(BeesRecruitingID.Bumblebee));
    }

    public void _CallMenu(GameObject _barrack)
    {
        barrack = _barrack.GetComponent<Barrack>();
    }

    public void _BuildingLVL_Up()
    {
        barrack._NextBuildingLevel();
    }
}
