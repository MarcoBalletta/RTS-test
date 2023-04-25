using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackerMovementSantaHeight : MonoBehaviour
{
    private Vector3 spawningPosition;
    [SerializeField] private float speedUp;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        spawningPosition = transform.position;
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.position += transform.up * speedUp * Input.GetAxis("Mouse Y") * Time.deltaTime;
        text.text = transform.position.y + " m";
    }

}
