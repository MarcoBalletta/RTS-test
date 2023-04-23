using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T: Component
{
    public T elementToSpawn;
    public uint numberToSpawn;
    public ETypeOfElementToSpawn elementToSpawnRetrieveDataFromGameManager;
}
