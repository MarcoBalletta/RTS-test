using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private SantaController santaSelected;
    private InputActions inputAction;

    private void Awake()
    {
        inputAction = new InputActions();
        inputAction.Enable();
        inputAction.Player.Select.performed += ClickedOnClickableEntity;
    }

    private void ClickedOnClickableEntity(InputAction.CallbackContext obj)
    {
        
    }

    private void Update()
    {
        
    }
}
