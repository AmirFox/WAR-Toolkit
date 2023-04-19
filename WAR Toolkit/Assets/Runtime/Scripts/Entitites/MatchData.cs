using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.ObjectData;

[CreateAssetMenu(fileName = "Match Data", menuName = "Custom Assets/Match Data")]
public class MatchData : ScriptableObject
{
    public MapData mapData;
    public GameType gameType;
    
    [Header("Player 1")]
    public FactionData faction1;
    public int startingResources1;
    public Vector2 spawnZonePosition1;
    public int spawnZoneSize1;

    [Header("Player 2")]
    public FactionData faction2;
    public int startingResources2;
    public Vector2 spawnZonePosition2;
    public int spawnZoneSize2;
}
