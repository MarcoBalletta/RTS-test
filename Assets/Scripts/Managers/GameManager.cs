using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{

    public delegate void OnFreeState();
    public OnFreeState onFreeState;

    public delegate void OnTacticalView();
    public OnTacticalView onTacticalView;

    public delegate void SantaSelectedInfos(SantaController santa);
    public SantaSelectedInfos onSantaSelectedInfos;
    
    public delegate void SantaSelectedMovement(SantaController santa);
    public SantaSelectedMovement onSantaSelectedMovement;

    public delegate void Selected2DPosition(Vector3 position);
    public Selected2DPosition onSelected2DPosition;

    public delegate void BefanaCaughtSanta(Entity santa, EnemyController befana);
    public BefanaCaughtSanta onBefanaCaughtSanta;

    private int levelDataIndex;
    [SerializeField]private LevelData levelDataSelected;
    private string pathLevelDataFolder = "LevelDataFolder/Level";

    public void SetLevelDataIndex(int number)
    {
        levelDataIndex = number;
        levelDataSelected = Resources.Load<LevelSO>(pathLevelDataFolder+levelDataIndex.ToString()).data;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Constants.SCENE_GAME_NAME);
    }
}
