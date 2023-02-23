using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;

public class Movable : MonoBehaviour, IMovable<DataTile>
{
    public DataTile CurrentTile { get; }
    public bool HasMoved { get; private set; }

    public int BaseMovementValue { get; }

    public void MoveToTile(DataTile tile)
    {
        //move to the given tile
        HasMoved = true;
    }

    public void TurnReset()
    {
        HasMoved = false;
    }
}
