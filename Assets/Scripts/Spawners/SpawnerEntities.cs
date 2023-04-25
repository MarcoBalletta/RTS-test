using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEntities : Spawner<Entity>
{

    [SerializeField] private float minBaseOffset;
    [SerializeField] private float maxBaseOffset;

    protected override void Start()
    {
        base.Start();
        switch (elementToSpawnRetrieveDataFromGameManager)
        {
            case ETypeOfElementToSpawn.santa:
                numberToSpawn = GameManager.instance.LevelDataSelected.santasInGameNumber;
                break;
            case ETypeOfElementToSpawn.befana:
                numberToSpawn = GameManager.instance.LevelDataSelected.befanasInGameNumber;
                break;
        }
        Spawn();
    }

    protected override void Spawn()
    {
        
        for(int i = 0; i< numberToSpawn; i++)
        {
            var position = GetRandomPosition();
            position.y = GetRandomHeight();
            var element = Instantiate(elementToSpawn, position, Quaternion.identity);
            switch (elementToSpawnRetrieveDataFromGameManager)
            {
                case ETypeOfElementToSpawn.santa:
                    GameManager.instance.AddSantaToList((element as SantaController));
                    break;
                case ETypeOfElementToSpawn.befana:
                    GameManager.instance.AddBefanaToList((element as EnemyController));
                    break;
            }
        }
    }

    private float GetRandomHeight()
    {
        return Random.Range(minBaseOffset, maxBaseOffset);
    }
}
