using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBuildings : Spawner<Building>
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        numberToSpawn = GameManager.instance.LevelDataSelected.housesInGameNumber;
        Spawn();
    }

    protected override void Spawn()
    {

        for (int i = 0; i < numberToSpawn; i++)
        {
            var position = GetRandomPosition();
            var element = Instantiate(elementToSpawn, position, Quaternion.identity);
            GameManager.instance.AddBuildingToList(element);
        }
    }
}
