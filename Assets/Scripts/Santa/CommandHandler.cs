using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    [SerializeField] List<Command> listOfCommands = new List<Command>();
    [SerializeField] private KeyCode appendCommandKey = KeyCode.LeftControl;
    private Command selectedCommand;
    private Vector3 positionPassed;
    private float baseOffsetPassed;
    private DestinationObject objectPassed;

    private IdleCommand idle;
    private MovingCommand moving;
    private PickingCommand picking;
    private DeliveringCommand delivering;
    private SantaController controller;

    private void Awake()
    {
        SetupCommands();
    }
    private void OnEnable()
    {
        controller = GetComponent<SantaController>();
        controller.onAddMovementCommand += SetValuesPassed;
        controller.onAddMovementCommand += TryToAddCommand;
        controller.onReachedDestination += RemoveCommandDone;
    }

    private void SetupCommands()
    {
        idle = new IdleCommand();
        moving = new MovingCommand();
        picking = new PickingCommand();
        delivering = new DeliveringCommand();
    }

    public void AddCommand(Command command)
    {
        listOfCommands.Add(command);
        Debug.Log("Added command " + listOfCommands.Count);
        if(listOfCommands.Count< 2) CheckWhichCommandExecute();
    }

    private void SetValuesPassed(Vector3 position, float baseOffset, DestinationObject destinationObject)
    {
        objectPassed = destinationObject;
        positionPassed = position;
        baseOffsetPassed = baseOffset;
    }

    public void RemoveCommand(Command command)
    {
        listOfCommands.Remove(command);
    }

    private void TryToAddCommand(Vector3 position, float baseOffset, DestinationObject destinationObject)
    {
        //SetValuesPassed(position, baseOffset, destinationObject);
        if (!CheckIfAddOrOverwriteCommand())
        {
            ClearValues();
        }
        ControlCommandsToAdd(destinationObject);
    }

    private void ClearValues()
    {
        listOfCommands.Clear();
        selectedCommand = null;
        Debug.Log("Clear values: " + listOfCommands.Count);
    }

    private void ControlCommandsToAdd(DestinationObject destinationObject)
    {
        AddCommand(moving);
        if (destinationObject != null)
        {
            switch (destinationObject.CommandType)
            {
                case ECommandType.picking:
                    AddCommand(picking);
                    break;
                case ECommandType.delivering:
                    AddCommand(delivering);
                    break;
            }
        }
    }

    private bool CheckIfAddOrOverwriteCommand()
    {
        Debug.Log(Input.GetKey(appendCommandKey) + " --- value input ctrl");
        return Input.GetKey(appendCommandKey);
    }

    private void CheckWhichCommandExecute()
    {
        //if (selectedCommand != null) return;
        selectedCommand = CheckNewCommand();
        selectedCommand?.Execute(controller, positionPassed, objectPassed, baseOffsetPassed);
    }

    private Command CheckNewCommand()
    {
        Debug.Log("Command result: " + (listOfCommands.Count > 0 ? listOfCommands[0] : null));
        return listOfCommands.Count > 0 ? listOfCommands[0] : null;
    }

    private void RemoveCommandDone()
    {
        if(listOfCommands.Count > 0) listOfCommands.RemoveAt(0);
        listOfCommands.TrimExcess();
        CheckWhichCommandExecute();
    }
}
