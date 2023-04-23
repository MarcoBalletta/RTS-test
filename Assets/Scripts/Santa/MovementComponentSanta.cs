using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponentSanta : MovementComponent
{

    //new SantaController controller;

    protected override void OnEnable()
    {
        base.OnEnable();
        controller = GetComponent<SantaController>();
        //(controller as SantaController).onStartMoving
    }

    private void Start()
    {
        //controller.onAddMovementCommand += 
    }


}
