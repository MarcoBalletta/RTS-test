using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level", menuName ="ScriptableObjects/CreateLevelData", order =0)]
public class LevelSO : ScriptableObject
{
    public LevelData data;
}

[System.Serializable]
public class LevelData
{
    public int number;
}
