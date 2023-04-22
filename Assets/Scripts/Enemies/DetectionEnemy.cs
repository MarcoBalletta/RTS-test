using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    private EnemyController enemy;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Entity player))
        {
            //enemy.TryToAlertEveryone(player);
            enemy.FoundPlayer(player);
        }
    }
}
