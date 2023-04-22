using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCommand : Command
{
    public override void Execute(SantaController player, Vector3 destination)
    {
        Debug.Log("Idle");
    }
}
