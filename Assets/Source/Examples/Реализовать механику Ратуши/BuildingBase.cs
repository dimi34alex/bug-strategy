using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    float healPoints = 0;
    [SerializeField] LayerMask layerMask;//слои, необходимо чтобы рейкаст игнорировал слои в которых находяться здания, и проходил скозь них
    Camera cam;
    int lvl = 1;//уровень здания
    bool spawnBuilding = true;//мы только что заспавнили здание?
    bool moveBuilding = true;//мы перемещаем здание?
    protected UI_Controller UI;//контроллер интерфеса
    Vector3 prevPosition;//предыдущая позиция

    virtual protected void onAwake()
    {
        cam = Camera.main;
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
    }

    virtual protected void onStart()
    {

    }

    virtual protected void onUpdate()
    {
        _MoveBuilding();
    }

    virtual protected void onFixedUpdate()
    {

    }

    virtual protected void _MoveBuilding()//перемещение здания по карте
    {
        if (moveBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100F, layerMask))//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
            {
                transform.position = ray.GetPoint(hit.distance);
                if (Input.GetButtonDown("Fire1"))//лкм
                {
                    moveBuilding = false;
                    spawnBuilding = false;
                }
                else if (Input.GetButtonDown("Fire2") && !spawnBuilding)//Если здание уже давно заспавнено и мы нажали пкм, то здание будет перемещено на исходную позицию
                {
                    moveBuilding = !moveBuilding;
                    transform.position = prevPosition;
                }
                else if (Input.GetButtonDown("Fire2") && spawnBuilding)//Если мы только что заспавнили здание и нажали пкм, то здание будет уничтожено
                {
                    Destroy(this.gameObject);
                    moveBuilding = !moveBuilding;
                }
            }
        }
    }

    public virtual void _DestroyBuilding()//снос здания, надо дороботать возращение ресурсов, вызывется через UI/UX
    {
        Destroy(gameObject);
    }

    public virtual void _LVL_UpBuilding()//повышение уровня здания
    {
        Debug.Log("Building LVL = " + ++lvl);
    }

    public virtual void _ReplaceBuilding()//перестановка здания, вызывется через UI/UX
    {
        moveBuilding = true;
        prevPosition = transform.position;
    }

    public virtual void _GetDamage(float damage)
    {
        healPoints -= damage;
        if (healPoints <= 0)
            _DestroyBuilding();
    }

    public virtual void _CallBuildingMenu(string windowName)//возможно стоит напрямую вызывать _SetWindow(), без этого посредника?
    {
        UI._SetWindow(windowName);
    }
}
