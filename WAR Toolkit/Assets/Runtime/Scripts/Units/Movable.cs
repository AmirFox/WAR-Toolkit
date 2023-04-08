using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using Zenject;

[RequireComponent(typeof(IUnit))]
public class Movable : MonoBehaviour, IMovable
{
    [Inject] private TurnManager _turnManager;
    [Inject] private IEventManager _eventManager;

    public Vector2 CurrentTile { get; }
    public bool HasMoved { get; private set; }

    [field:SerializeField] public int BaseMovementValue { get; private set; }

    public void MoveToTile(Vector2 tile)
    {
        //move to the given tile
        HasMoved = true;
    }

    public void TurnReset()
    {
        HasMoved = false;
    }

    private void OnMouseDown() {
        if(_turnManager.CurrentPhase == WarToolkit.Core.Enums.Phase.MOVEMENT)
        {
            SelectionEventArgs<Movable> movableArgs = new SelectionEventArgs<Movable>(this);
            _eventManager.TriggerEvent(Constants.EventNames.MOVABLE_SELECTED, movableArgs);
        }
    }
}
