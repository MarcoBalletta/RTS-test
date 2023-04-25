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
    public uint numberIndex;
    [Min (3)]
    public uint befanasInGameNumber;
    public uint santasInGameNumber;
    [Min (20)]
    public uint giftsInGameNumber;
    public uint minGiftsToDeliver;
    public float weightFromEachGift;
    [Min(10)]
    public uint housesInGameNumber;
    public float time;

    public LevelData(uint index, uint befanas, uint santas, uint gifts, uint minGifts, uint houses, uint timeInGame)
    {
        numberIndex = index;
        befanasInGameNumber = befanas;
        santasInGameNumber = santas;
        giftsInGameNumber = gifts;
        minGiftsToDeliver = minGifts;
        housesInGameNumber = houses;
        time = timeInGame;
    }
}
