using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestinationObject : MonoBehaviour, IRightClickableUp
{

    [SerializeField] ECommandType commandType;

    public ECommandType CommandType { get => commandType;}

    public virtual void RightClicked(PlayerController player, Vector3 position)
    {
        //player.SantaSelected.onAddMovementCommand(new Vector3(transform.position.x, 0, transform.position.z), transform.position.y, this);
    }
}
