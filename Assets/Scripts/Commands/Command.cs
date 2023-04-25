using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Command
{

    private Vector3 startPosition;
    private Vector3 destinationPosition;
    private DestinationObject objectPassed;
    private SantaController santa;
    private float baseOffset;

    public Vector3 DestinationPosition { get => destinationPosition;}
    public DestinationObject ObjectPassed { get => objectPassed;}
    public SantaController Santa { get => santa;}
    public float BaseOffset { get => baseOffset;}
    public Vector3 StartPosition { get => startPosition;}

    public virtual void Setup(SantaController player, Vector3 destination, DestinationObject destinationObject, float baseOffset, Vector3 startPos)
    {
        santa = player;
        destinationPosition = destination;
        objectPassed = destinationObject;
        this.baseOffset = baseOffset;
        startPosition = startPos;
    }

    public abstract void Execute(SantaController player, Vector3 destination, DestinationObject destinationObject, float baseOffset);
}

[System.Serializable]
public enum CommandTypeSanta
{
    moving,
    picking,
    delivering,
    idle,
}