using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private SantaController santaSelected;
    private InputActions inputAction;
    [SerializeField] private LayerMask layerClickLeft;
    [SerializeField] private LayerMask layerClickRight;
    private bool hasSelectedPositionGround = false;
    private Vector3 positionGroundSelected;
    private Vector3 position2DSelected;

    [SerializeField] private TrackerMovementSantaHeight tracker;
    private TrackerMovementSantaHeight trackerInstance;

    public SantaController SantaSelected { get => santaSelected; set => santaSelected = value; }

    private void Awake()
    {
        inputAction = new InputActions();
        inputAction.Enable();
        inputAction.Player.SelectLeftClick.performed += ClickedLeftButton;
        inputAction.Player.SelectRightClick.performed += ClickedRightButton;
        GameManager.instance.onSantaSelectedMovement += SetSantaSelected;
        GameManager.instance.onSelected2DPosition += Selected2DPositionGround;
        GameManager.instance.onSelected2DPosition += SpawnTrackerBall;
    }

    private void ClickedLeftButton(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerClickLeft,  QueryTriggerInteraction.Collide))
        {
            hit.collider.gameObject.TryGetComponent(out ILeftClickable entityClickable);
            if (entityClickable != null) entityClickable.LeftClicked(this, hit.point);
        }
    }

    private void ClickedRightButton(InputAction.CallbackContext obj)
    {
        if(santaSelected != null && hasSelectedPositionGround)
        {
            Debug.Log("Create Command");
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerClickRight, QueryTriggerInteraction.Ignore))
            //{
            //    hit.collider.gameObject.TryGetComponent(out DestinationObject entityClickable);
            //    santaSelected.onAddMovementCommand(position2DSelected, trackerInstance.transform.position.y, entityClickable);
            //    Destroy(trackerInstance.gameObject, 0.1f);
            //}
            santaSelected.onAddMovementCommand(position2DSelected, trackerInstance.transform.position.y, null);
            ClearPositionValues();
            Destroy(trackerInstance.gameObject, 0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1) && santaSelected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10000, layerClickRight, QueryTriggerInteraction.Collide))
            {
                hit.collider.gameObject.TryGetComponent(out DestinationObject entityClickable);
                Debug.Log("Right click : " + entityClickable);
                if (entityClickable != null)
                {
                    entityClickable.RightClicked(this, hit.point);
                }
            }
        }
    }

    private void ClearPositionValues()
    {
        hasSelectedPositionGround = false;
        santaSelected = null;
    }

    public void SetSantaSelected(SantaController selected)
    {
        santaSelected = selected;
    }

    public void Selected2DPositionGround(Vector3 position)
    {
        position2DSelected = position;
        hasSelectedPositionGround = true;
    }

    private void SpawnTrackerBall(Vector3 position)
    {
        trackerInstance = Instantiate(tracker, position, Quaternion.identity, transform);
    }

}
