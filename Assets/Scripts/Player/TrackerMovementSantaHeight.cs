using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerMovementSantaHeight : MonoBehaviour
{
    private Vector3 spawningPosition;
    [SerializeField] private float speedUp;

    private void Start()
    {
        spawningPosition = transform.position;
    }

    private void Update()
    {
        transform.position += transform.up * speedUp * Input.GetAxis("Mouse Y") * Time.deltaTime;
    }

}
