using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Spawner<T> : MonoBehaviour where T: Component
{
    public T elementToSpawn;
    public uint numberToSpawn;
    public ETypeOfElementToSpawn elementToSpawnRetrieveDataFromGameManager;
    protected NavMeshSurface navMesh;

    protected virtual void Start()
    {
        navMesh = FindObjectOfType<NavMeshSurface>();
    }

    protected abstract void Spawn();
    protected virtual Vector3 GetRandomPosition()
    {
        var navmeshBounds = navMesh.navMeshData.sourceBounds;
        var x = Random.Range(navmeshBounds.min.x, navmeshBounds.max.x);
        var z = Random.Range(navmeshBounds.min.z, navmeshBounds.max.z);
        return new Vector3(x, 0, z);
    }
}
