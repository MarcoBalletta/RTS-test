using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGifts : Spawner<PickableItem>
{
    [SerializeField]private float minBaseOffset;
    [SerializeField]private float maxBaseOffset;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        numberToSpawn = GameManager.instance.LevelDataSelected.giftsInGameNumber;
        Spawn();
    }

    protected override void Spawn()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var position = GetRandomPosition();
            position.y = GetRandomHeight();
            var element = Instantiate(elementToSpawn, position, Quaternion.identity);
            GameManager.instance.AddPickableToList(element);
        }
    }

    private float GetRandomHeight()
    {
        return Random.Range(minBaseOffset, maxBaseOffset);
    }

}
