using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingCommand : Command
{
    public override void Execute(SantaController player, Vector3 destination)
    {
        player.onMoveTo(destination);
    }

}