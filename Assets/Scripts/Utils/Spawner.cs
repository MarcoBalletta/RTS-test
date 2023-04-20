using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Spawner : MonoBehaviour
{

    public IPickable pickable;
    public Entity entityToSpawn;
    public int numberToSpawn;
    
    private void Start()
    {
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        for(int i= 0; i< numberToSpawn; i++)
        {
            //spawn in space 
        }
    }
}
