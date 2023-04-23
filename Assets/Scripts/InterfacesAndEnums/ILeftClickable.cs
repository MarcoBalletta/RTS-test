using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeftClickable
{
    public abstract void LeftClicked(PlayerController player, Vector3 clickPosition);
}
