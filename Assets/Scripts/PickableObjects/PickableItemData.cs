using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/CreatePickableData", order = 0)]
public class PickableItemData : ScriptableObject
{
    [SerializeField] private Building destinationBuilding;
    [SerializeField] private PickableItem dropObject;

    public Building DestinationBuilding { get => destinationBuilding;}
    public PickableItem DropObject { get => dropObject;}
}
