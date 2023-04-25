using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(CommandHandler))]
public class SantaController : Entity
{
    [SerializeField] private float showPathTime;
    [SerializeField] private Material selectedPathMaterial;
    [SerializeField] private Material nonSelectedPathMaterial;
    //private List<LineRenderer> lines = new List<LineRenderer>();
    //[SerializeField]private GameObject linePrefab;
    private LineRenderer lineRenderer;
    private StateManagerSanta stateManager;
    private CommandHandler commandHandler;
    new protected MovementComponentSanta movingComponent;
    private InventoryComponent inventoryComponent;

    public delegate void OnAddMovementCommand(Vector3 position, float baseOffset, DestinationObject clickedEntity);
    public OnAddMovementCommand onAddMovementCommand;

    public delegate void OnStartMoving(Vector3 position, float baseOffset, DestinationObject clickedEntity);
    public OnStartMoving onStartMoving;

    public delegate void ReachedDestination();
    public ReachedDestination onReachedDestination;

    protected override void Awake()
    {
        CheckOrAddComponent(out colliderEntity);
        CheckOrAddComponent(out rigidbodyEntity);
        CheckOrAddComponent(out movingComponent);
        CheckOrAddComponent(out lineRenderer);
    }
    protected override void OnEnable()
    {
        movingComponent.Agent.baseOffset = transform.position.y;
        GameManager.instance.onSantaSelectedInfos += TogglePath;
        onReachedDestination += RefreshPathOnReachedDestination;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CheckOrAddComponent(out stateManager);
        CheckOrAddComponent(out commandHandler);
        CheckOrAddComponent(out inventoryComponent);
    }

    public override float CalculateTimingLerpAdjustingHeight(float baseOffset)
    {
        return Vector3.Distance(transform.position, new Vector3(movingComponent.Agent.destination.x, baseOffset, movingComponent.Agent.destination.z)) / speed * Time.deltaTime;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("santa Selected movement");
            GameManager.instance?.onSantaSelectedMovement(this);
        }
    }

    public override void LeftClicked(PlayerController player, Vector3 position)
    {
        base.LeftClicked(player, position);
        Debug.Log("Clicked on Santa : " + this.name);
        GameManager.instance?.onSantaSelectedInfos(this);
    }

    public void CheckGiftForBuilding(Building building)
    {
        foreach(var item in inventoryComponent.items.ToArray())
        {
            if(item is PickableItem && building.CheckIfItemIsInListItemsToDeliver(item))
            {
                inventoryComponent.DropItem(item);
                building.RemoveItemFromList(item);
                SetSantaSpeed();
                GameManager.instance.onGiftDelivered();
            }
        }
    }

    public void PickupItem(PickableItem item)
    {
        if (inventoryComponent.CanPickup())
        {
            inventoryComponent.PickupItem(item);
            SetSantaSpeed();
            item.gameObject.SetActive(false);
        }
    }

    private void SetSantaSpeed()
    {
        movingComponent.Agent.speed -= GetInventoryItems().Count * GameManager.instance.LevelDataSelected.weightFromEachGift;
    }

    public List<PickableItem> GetInventoryItems()
    {
        return inventoryComponent.items;
    }

    private void ShowPath(SantaController santa)
    {
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = true;
        if (commandHandler.ListOfCommands.Count < 1) return;
        Debug.Log(commandHandler.ListOfCommands.Count + " --- moving commands");
        List<Command> movingCommands = new List<Command>();
        movingCommands = commandHandler.ListOfCommands.Where(x => x is MovingCommand).ToList();
        Debug.Log(movingCommands.Count + " --- moving commands");
        lineRenderer.positionCount = movingCommands.Count+1;

        for (int i = 0; i < movingCommands.Count; i++)
        {
            //var lineRendererObj = Instantiate(linePrefab, transform);
            //lineRendererObj.AddComponent<LineRenderer>();
            //var lineRenderer = lineRendererObj.GetComponent<LineRenderer>();
            lineRenderer.material = selectedPathMaterial;
            if(i == 0)
            {
                lineRenderer.startColor = selectedPathMaterial.color;
                lineRenderer.endColor = selectedPathMaterial.color;
            }
            else
            {
                lineRenderer.startColor = nonSelectedPathMaterial.color;
                lineRenderer.endColor = nonSelectedPathMaterial.color;
            }

            //cambiare usando Calculate path e prendendo i corners del path per un ciclo for
            lineRenderer.SetPosition(i, new Vector3( movingCommands[i].StartPosition.x, movingCommands[i].BaseOffset, movingCommands[i].StartPosition.z));
            if(i == movingCommands.Count - 1)
            {
                lineRenderer.SetPosition(i + 1, new Vector3(movingCommands[i].DestinationPosition.x, movingCommands[i].BaseOffset, movingCommands[i].DestinationPosition.z));
            }
            //lines.Add(lineRenderer);
        }
        //StartCoroutine(ShowPathRoutine());
    }

    public void RefreshPathOnReachedDestination()
    {
        //if (lines.Capacity > 0 && lines[0].enabled) ShowPath(this);
        if (lineRenderer.enabled) ShowPath(this);
    }

    private void TogglePath(SantaController santa)
    {
        lineRenderer.enabled = !lineRenderer.enabled;
        RefreshPathOnReachedDestination();
    }

    //private IEnumerator ShowPathRoutine()
    //{
    //    //foreach(var lineRenderer in lines)
    //    //{
    //    //    lineRenderer.enabled = true;
    //    //}
    //    //yield return new WaitForSeconds(showPathTime);
    //    //foreach (var lineRenderer in lines)
    //    //{
    //    //    lineRenderer.enabled = false;
    //    //}

    //    lineRenderer.enabled = true;
    //    yield return new WaitForSeconds(showPathTime);
    //    lineRenderer.enabled = false;
    //}

    public float GetBaseOffset()
    {
        return movingComponent.Agent.baseOffset;
    }
}
