using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private SantaController santaSelected;
    private InputActions inputAction;
    [SerializeField] private LayerMask layerClick;

    private void Awake()
    {
        inputAction = new InputActions();
        inputAction.Enable();
        inputAction.Player.Select.performed += ClickedOnClickableEntity;
    }

    private void ClickedOnClickableEntity(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000, layerClick,  QueryTriggerInteraction.Ignore))
        {
            hit.collider.gameObject.TryGetComponent<IClickable>(out IClickable clickableInterface);
            if (clickableInterface != null) clickableInterface.ClickedOn(this);
        }
    }

    public void SantaSelected(SantaController selected)
    {
        santaSelected = selected;
    }
}
