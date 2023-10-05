using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenStorage : ResourceSourceBase
{
    protected ResourceStorage Pollen = new ResourceStorage(100, 100);
    public float MaxPollen => Pollen.Capacity;
    public float CurrentPollen => Pollen.CurrentValue;
    public int timer;
    public bool isPollinated = false;
    public GameObject Model;
    public GameObject pollinatedModel;
    
    public virtual void ExtractPollen(int extracted)
    {
        Pollen.ChangeValue(-extracted);
        Debug.Log("�������� ������ - " + CurrentPollen);

        if (CurrentPollen <= 0)
        {
            isPollinated = true;
            pollinatedModel.SetActive(true);
            Model.SetActive(false);
            StartCoroutine(StartRePollinating(timer));
        }

    }

    private IEnumerator StartRePollinating(int duration)
    {
        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        isPollinated = false;
        pollinatedModel.SetActive(false);
        Model.SetActive(true);
    }
}
