using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public delegate void OnFreeState();
    public OnFreeState onFreeState;

    public delegate void OnTacticalView();
    public OnTacticalView onTacticalView;

    public delegate void SantaSelectedInfos(SantaController santa);
    public SantaSelectedInfos onSantaSelectedInfos;
    
    public delegate void SantaSelectedMovement(SantaController santa);
    public SantaSelectedMovement onSantaSelectedMovement;

    public delegate void Selected2DPosition(Vector3 position);
    public Selected2DPosition onSelected2DPosition;



}
