using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUIScript : MonoBehaviour
{
    // ������ ������
    public GameObject[] saveSlots; // ����� ����� �������� ����� ���������� ����� ���������
    public GameObject[] checkmarks; // ���� ��������� ������� ��� ������� �����

    // ��������� ����
    private int selectedSlot = -1;

    // �����, ������� ���������� ��� ������� �� ����
    public void SelectSlot(int slotIndex)
    {
        // ��������� ������� �� ���� ������
        for (int i = 0; i < checkmarks.Length; i++)
        {
            checkmarks[i].SetActive(false);
        }

        // �������� ������� �� ��������� �����
        checkmarks[slotIndex].SetActive(true);
        selectedSlot = slotIndex;
    }
}