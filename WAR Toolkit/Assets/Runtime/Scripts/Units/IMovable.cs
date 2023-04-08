using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.ObjectData;

public interface IMovable
{
    Vector2 CurrentTile{ get; }

    /// <summary>
    /// Whether this object has moved or not.
    /// </summary>
    bool HasMoved { get; }

    /// <summary>
    /// How far the object can move on the map per turn.
    /// </summary>
    int BaseMovementValue { get; }

    /// <summary>
    /// Move this object to the target tile.
    /// </summary>
    /// <param name="tile">the target tile</param>
    void MoveToTile(Vector2 tile);

    void TurnReset();
}
