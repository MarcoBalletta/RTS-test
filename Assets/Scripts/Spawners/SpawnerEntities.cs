using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEntities : Spawner<Entity>
{

    [SerializeField] private float minBaseOffset;
    [SerializeField] private float maxBaseOffset;
    private NavMeshSurface navMesh;

    private void Start()
    {
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

    public void Spawn()
    {
        navMesh = FindObjectOfType<NavMeshSurface>();
        for(int i = 0; i< numberToSpawn; i++)
        {
            var element = Instantiate(elementToSpawn, GetRandomPosition(), Quaternion.identity);
        }
    }

    public Vector3 GetRandomPosition()
    {
        var navmeshBounds = navMesh.navMeshData.sourceBounds;
        var x = Random.Range(navmeshBounds.min.x, navmeshBounds.max.x);
        var y = Random.Range(minBaseOffset, maxBaseOffset);
        var z = Random.Range(navmeshBounds.min.z, navmeshBounds.max.z);
        return new Vector3(x, y, z);
    }
}
