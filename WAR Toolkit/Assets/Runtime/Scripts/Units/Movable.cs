using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;

public class Movable : MonoBehaviour, IMovable<Tile>
{
    public Tile CurrentTile { get; }
    public bool HasMoved { get; private set; }

    public int BaseMovementValue { get; }

    public void MoveToTile(Tile tile)
    {
        //move to the given tile
        HasMoved = true;
    }

    public void TurnReset()
    {
        HasMoved = false;
    }
}
