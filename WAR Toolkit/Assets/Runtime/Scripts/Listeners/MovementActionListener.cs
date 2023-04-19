using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;

public class MovementActionListener : PlayerActionListener
{
    [Zenject.Inject] private ITileQuery _tileQuery;
    [Zenject.Inject] private IMapController _mapController;
    private IMovable _selectedMovable;
    private Vector2 _selectedTile;
    private List<Vector2> _movementRange;

    protected override Phase phase => Phase.MOVEMENT;

    public MovementActionListener(int playerIndex, IEventManager eventManager, ITileQuery tileQuery) : base(playerIndex, eventManager) { }

    protected override void StartListening()
    {
        _eventManager.StartListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
        _eventManager.StartListening(Constants.EventNames.MOVABLE_SELECTED, OnMovableSelected);
    }

    protected override void StopListening()
    {
        _eventManager.StopListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
        _eventManager.StopListening(Constants.EventNames.MOVABLE_SELECTED, OnMovableSelected);
        ClearSelection();
    }

    private void OnMovableSelected(IArguements args)
    {
        if(args is UnitSelectedEventArgs selectionArgs)
        {
            if(selectionArgs.MovableComponent == null)
            {
                return;
            }
            if(selectionArgs.MovableComponent.HasMoved)
            {
                return;
            }            

            _selectedMovable = selectionArgs.MovableComponent as IMovable;
            _movementRange = _tileQuery.QueryRadius(_selectedMovable.CurrentTile, (int)_selectedMovable.BaseMovementValue);
            _mapController.HighlightTiles(_movementRange.ToArray());
            }
    }

    private void OnTileSelected(IArguements args)
    {
        if(args is SelectionEventArgs<Vector2> selectionArgs)
        {
            if(_selectedMovable != null)
            {
                if(_movementRange.Contains(_selectedTile))
                {
                    _selectedMovable.MoveToTile(_selectedTile);
                    ClearSelection();
                }
            }
        }
    }

    private void SetSelection(Vector2 origin, int range)
    {
        _movementRange = _tileQuery.QueryRadius(origin, range);
        _mapController.HighlightTiles(_movementRange.ToArray());
    }

    private void ClearSelection()
    {
        _mapController.ClearHighlights();
        _movementRange?.Clear();
        _selectedMovable = null;
        _selectedTile = default;
    }
}