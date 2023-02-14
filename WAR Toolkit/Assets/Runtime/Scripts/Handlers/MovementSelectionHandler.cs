using System.Collections;
using System.Collections.Generic;
using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;

public class MovementSelectionHandler<T> : BaseSelectionHandler where T : ITile
{
    private ITileQuery<T> _tileQuery;
    private IMovable<T> _selectedMovable;
    private T _selectedTile;
    private List<T> _movementRange;

    protected override Phase phase => Phase.MOVEMENT;

    public MovementSelectionHandler(int playerIndex, IEventManager eventManager, ITileQuery<T> tileQuery) : base(playerIndex, eventManager) { }

    protected override void AddListeners()
    {
        _eventManager.StartListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
        _eventManager.StartListening(Constants.EventNames.MOVABLE_SELECTED, OnMovableSelected);
    }

    protected override void RemoveListeners()
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
            
            _selectedMovable = selectionArgs.MovableComponent as IMovable<T>;
            _movementRange = _tileQuery.QueryRadius(_selectedMovable.CurrentTile, (int)_selectedMovable.BaseMovementValue);
                foreach(T tile in _movementRange)
                {
                    tile.SetHighlight(true);
                }
            }
    }

    private void OnTileSelected(IArguements args)
    {
        if(args is SelectionEventArgs<T> selectionArgs)
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

    private void SetSelection(T origin, int range)
    {
        _movementRange = _tileQuery.QueryRadius(origin, range);
        foreach(T tile in _movementRange)
        {
            tile.SetHighlight(true);
        }
    }

    private void ClearSelection()
    {
        foreach(T tile in _movementRange)
        {
            tile.SetHighlight(false);
        }
        _movementRange?.Clear();
        _selectedMovable = null;
        _selectedTile = default;
    }
}