using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUIScript : MonoBehaviour
{
    // Массив слотов
    public GameObject[] saveSlots; // Здесь нужно добавить слоты сохранений через инспектор
    public GameObject[] checkmarks; // Сюда добавляем галочки для каждого слота

    // Выбранный слот
    private int selectedSlot = -1;

    // Метод, который вызывается при нажатии на слот
    public void SelectSlot(int slotIndex)
    {
        // Отключаем галочки на всех слотах
        for (int i = 0; i < checkmarks.Length; i++)
        {
            checkmarks[i].SetActive(false);
        }

        // Включаем галочку на выбранном слоте
        checkmarks[slotIndex].SetActive(true);
        selectedSlot = slotIndex;
    }
}