using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{

    private List<PickableItem> pickableItems = new List<PickableItem>();
    private List<Building> buildings = new List<Building>();
    private List<SantaController> santas = new List<SantaController>();
    private List<EnemyController> befanas = new List<EnemyController>();
    [SerializeField] private float percentageOfGivingPickableToBuilding = 30;
    private int levelDataIndex;
    [SerializeField]private float highlightInfosTime;
    [SerializeField]private float inventoryPanelShowTime;
    private LevelData levelDataSelected;
    private string pathLevelDataFolder = "LevelDataFolder/Level";

    private uint giftsDelivered;
    private uint santasAvailable;

    public delegate void OnFreeState();
    public OnFreeState onFreeState;

    public delegate void OnTacticalView();
    public OnTacticalView onTacticalView;

    public delegate void OnGiftTaken();
    public OnGiftTaken onGiftPicked;

    public delegate void OnGiftDelivered();
    public OnGiftTaken onGiftDelivered;

    public delegate void SantaSelectedInfos(SantaController santa);
    public SantaSelectedInfos onSantaSelectedInfos;
    
    public delegate void SantaSelectedMovement(SantaController santa);
    public SantaSelectedMovement onSantaSelectedMovement;

    public delegate void Selected2DPosition(Vector3 position);
    public Selected2DPosition onSelected2DPosition;

    public delegate void BefanaFoundSanta(Entity santa, EnemyController befana);
    public BefanaFoundSanta onBefanaFoundSanta;

    public delegate void BefanaCaughtSanta(Entity santa, EnemyController befana);
    public BefanaCaughtSanta onBefanaCaughtSanta;

    public delegate void UpdateTimer(float value);
    public UpdateTimer onUpdateTimer;

    public delegate void StartGameEvent();
    public StartGameEvent onStartGame;

    public delegate void EndGame(bool victory);
    public EndGame onEndGame;


    public LevelData LevelDataSelected { get => levelDataSelected; }
    public uint SantasAvailable { get => santasAvailable; set => santasAvailable = value; }
    public float HighlightInfosTime { get => highlightInfosTime;}
    public float InventoryPanelShowTime { get => inventoryPanelShowTime;}

    protected override void Awake()
    {
        base.Awake();
        onStartGame += StartTimer;
        onStartGame += InitDataFromLevelSelected;
        onGiftDelivered += GiftDelivered;
        onBefanaCaughtSanta += DecreaseSanta;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(Constants.SCENE_MENU_NAME);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += InitializeGameScene;
        //onStartGame();
    }

    public void SetLevelDataIndex(int number)
    {
        levelDataIndex = number;
        levelDataSelected = Resources.Load<LevelSO>(pathLevelDataFolder+levelDataIndex.ToString()).data;
        santasAvailable = levelDataSelected.santasInGameNumber;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Constants.SCENE_GAME_NAME);
    }

    private void InitializeGameScene(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name == Constants.SCENE_GAME_NAME)
        {
            //start timer
            onStartGame();
        }
    }

    private void InitDataFromLevelSelected()
    {
        santasAvailable = levelDataSelected.santasInGameNumber;

    }

    public void AddPickableToList(PickableItem pickableItem) 
    {
        pickableItems.Add(pickableItem);
        if (CheckIfCanAssignPickablesToBuildings()) AssignPickablesToBuildings();
    }

    public void AddBuildingToList(Building building) 
    {
        buildings.Add(building);
        if (CheckIfCanAssignPickablesToBuildings()) AssignPickablesToBuildings();
    }

    public void AddSantaToList(SantaController santa) 
    {
        santas.Add(santa);
    }

    public void AddBefanaToList(EnemyController enemy) 
    {
        befanas.Add(enemy);
    }

    private bool CheckIfCanAssignPickablesToBuildings()
    {
        return pickableItems.Count == levelDataSelected.giftsInGameNumber && buildings.Count == levelDataSelected.housesInGameNumber;
    }

    private void AssignPickablesToBuildings()
    {
        foreach(PickableItem pickable in pickableItems)
        {
            bool everyBuildingHasOnePickable = false;
            bool assignedPickeable = false;
            foreach(Building building in buildings)
            {
                if(building.ItemsToDeliver.Count == 0)
                {
                    building.ItemsToDeliver.Add(pickable);
                    pickable.DestinationBuilding = building;
                    assignedPickeable = true;
                    break;
                }
                everyBuildingHasOnePickable = true;
            }

            if (assignedPickeable) continue;
            if (everyBuildingHasOnePickable)
            {
                float offsetPercentage = 0;
                do
                {
                    var randomIndex = Random.Range(0, buildings.Count);
                    if (RandomDecideIfPickableGoesToBuilding(offsetPercentage))
                    {
                        buildings[randomIndex].ItemsToDeliver.Add(pickable);
                        pickable.DestinationBuilding = buildings[randomIndex];
                        assignedPickeable = true;
                    }
                    offsetPercentage += 5;
                }while (!assignedPickeable);
            }
        }
    }

    private bool RandomDecideIfPickableGoesToBuilding(float offset)
    {
        var percentage = Random.Range(0, 100);
        return percentage < percentageOfGivingPickableToBuilding + offset;
    }

    private void StartTimer()
    {
        StartCoroutine(StartTimerRoutine());
    }

    private IEnumerator StartTimerRoutine()
    {
        Debug.Log("StartTimer");
        var actualTimer = levelDataSelected.time;
        while(actualTimer > 0)
        {
            onUpdateTimer(actualTimer);
            Debug.Log("Timer");
            yield return new WaitForSeconds(1f);
            actualTimer--;
            onUpdateTimer(actualTimer);
        }
        onEndGame(CheckIfMinimumGiftsAreDelivered());
    }

    private void GiftDelivered()
    {
        giftsDelivered++;
    }

    private void DecreaseSanta(Entity santa, EnemyController befana)
    {
        santasAvailable--;
        CheckIfSantasAreAlive();
    }

    private void CheckIfSantasAreAlive()
    {
        if (santasAvailable <= 0) onEndGame(CheckIfMinimumGiftsAreDelivered());
    }

    public bool CheckIfMinimumGiftsAreDelivered()
    {
        return giftsDelivered >= levelDataSelected.minGiftsToDeliver;
    }
}
