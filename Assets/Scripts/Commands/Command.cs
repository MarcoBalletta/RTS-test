using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Command
{
    public abstract void Execute(SantaController player, Vector3 destination);
}

[System.Serializable]
public enum CommandTypeSanta
{
    moving,
    picking,
    delivering,
    idle,
}