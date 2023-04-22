using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StateManagerCamera))]
public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float speedY;
    [SerializeField] private float speedX;
    [SerializeField] private float speedZ;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    private float velocityX;
    private float velocityY;
    private float velocityZ;
    private float rotationX;
    private float rotationY;
    InputActions inputActions;
    Camera cameraGame;
    private Vector2 currentRotation;
    private StateManagerCamera stateManagerCamera;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        cameraGame = Camera.main;
        stateManagerCamera = GetComponent<StateManagerCamera>();
        GameManager.instance.onFreeState += FreeStateCursorMode;
        GameManager.instance.onTacticalView += TactivalViewStateCursorMode;
        inputActions.Camera.TacticalView.performed += ToggleStateCamera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(stateManagerCamera.currentState?.nameOfState == Constants.FREE_STATE_STATE)
        {
            velocityX = inputActions.Camera.MovementHorizontal.ReadValue<float>();
            velocityZ = inputActions.Camera.MovementForward.ReadValue<float>();
            velocityY = inputActions.Camera.MovementVertical.ReadValue<float>();

            cameraGame.transform.Translate((velocityX * speedX * Vector3.right + velocityY * speedY * Vector3.up + velocityZ * speedZ * Vector3.forward) * Time.deltaTime, cameraGame.transform);

            rotationX = sensitivityX * -Input.GetAxis("Mouse Y");
            currentRotation.x += rotationX;
            rotationY = sensitivityY * Input.GetAxis("Mouse X");
            currentRotation.y += rotationY;
            cameraGame.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
    }

    private void FreeStateCursorMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void TactivalViewStateCursorMode()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ToggleStateCamera(InputAction.CallbackContext context)
    {
        if(stateManagerCamera.currentState.nameOfState == Constants.FREE_STATE_STATE)
        {
            stateManagerCamera.OnChangeState(Constants.TACTICAL_VIEW_STATE);
        }
        else
        {
            stateManagerCamera.OnChangeState(Constants.FREE_STATE_STATE);
        }
    }
}
