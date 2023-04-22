using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    List<Command> listOfCommands = new List<Command>();
    private IdleCommand idle;
    private MovingCommand moving;
    private PickingCommand picking;
    private DeliveringCommand delivering;

    private void Awake()
    {
        SetupCommands();
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
    }

    public void RemoveCommand(Command command)
    {
        listOfCommands.Remove(command);
    }
}
