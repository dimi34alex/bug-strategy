using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : ConstructionBase
{
    public override ConstructionID ConstructionID => ConstructionID.Town_Hall;

    #region  Main
    [SerializeField] float maxHealPoints = 1000;
    float healPoints = 1000;

    [SerializeField] int maxTrees = 0;
    [SerializeField] int maxFlowers = 0;
    [SerializeField] int maxPlants = 0;
    [SerializeField] int maxWax = 0;

    int trees = 0;
    int flowers = 0;
    int plants = 0;
    int wax = 0;//воск
    #endregion

    #region Workers Bees
    public delegate void Alarm();
    public static event Alarm WorkerBeeAlarm;

    [SerializeField] GameObject beePrefab;//префаб рабочей пчелы
    [SerializeField] Transform spawnPositionOfWorkerBee;//координаты флага, на котором спавняться рабочие пчелы

    [SerializeField] int maxWorkerBeesNumber = 0;
    int WorkerBeesNumber = 0;
    #endregion

    #region Other
    [SerializeField] LayerMask layerMask;//слои, необходимо для премещения здания
    Camera cam;
    int lvl = 1;//уровень здания
    bool replaceBuilding = false;//мы перемещаем здание?
    protected UI_Controller UI;//контроллер интерфеса
    Vector3 prevPosition;//предыдущая позиция, нужно для отмены перемещения здания
    #endregion

    protected override void OnAwake()
    {
        base.OnAwake();
        cam = Camera.main;
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
    }

    void Update()
    {
        _MoveBuilding();
    }

    private void _MoveBuilding()//перемещение здания по карте
    {
        if (replaceBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100F, layerMask))//если рэйкаст сталкиваеться с чем нибудь, задаем зданию позицию точки столкновения рэйкаста
            {
                transform.position = FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(ray.GetPoint(hit.distance));

                if (Input.GetButtonDown("Fire1"))//лкм
                {
                    replaceBuilding = false;
                }
                else if (Input.GetButtonDown("Fire2"))//Если здание уже давно заспавнено и мы нажали пкм, то здание будет перемещено на исходную позицию
                {
                    replaceBuilding = !replaceBuilding;
                    transform.position = prevPosition;
                }
            }
        }
    }


    public void _SpawnWorkerBee()
    {
        if (WorkerBeesNumber < maxWorkerBeesNumber)
        {
            GameObject news = Instantiate(beePrefab, spawnPositionOfWorkerBee.position, spawnPositionOfWorkerBee.rotation);
            WorkerBeesNumber++;
        }
        else
        {
            Debug.Log("Error: you have max number of workers bees");
        }
    }
    public void _WorkerBeeAlarmer()
    {
        WorkerBeeAlarm?.Invoke();
    }
    public void _AddTrees(int addTrees)
    {
        if (trees + addTrees >= maxTrees)
            trees = maxTrees;
        else
            trees += addTrees;
    }
    public void _AddFlowers(int addFlowers)
    {
        if (flowers + addFlowers >= maxFlowers)
            flowers = maxFlowers;
        else
            flowers += addFlowers;
    }
    public void _AddPlants(int addPlants)
    {
        if (plants + addPlants >= maxPlants)
            plants = maxPlants;
        else
            plants += addPlants;
    }


    public void _CallBuildingMenu(string windowName)//вызов меню здания
    {
        UI._SetWindow(windowName);
    }
    public void _DestroyBuilding()//снос здания, надо дороботать возращение ресурсов, вызывется через UI/UX
    {
        Destroy(gameObject);
    }
    public void _LVL_UpBuilding()//повышение уровня здания, вызывется через UI/UX
    {
        Debug.Log("Building LVL = " + ++lvl);
    }
    public void _ReplaceBuilding()//перестановка здания, вызывется через UI/UX (перестоновка вообще должна быть????)
    {
        replaceBuilding = true;
        prevPosition = transform.position;
    }

    public void _GetDamage(float damage)//получение урона
    {
        healPoints -= damage;
        if (healPoints <= 0)
            Destroy(gameObject);
    }

}
