using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public delegate void OnFreeState();
    public OnFreeState onFreeState;

    public delegate void OnTacticalView();
    public OnTacticalView onTacticalView;

}
