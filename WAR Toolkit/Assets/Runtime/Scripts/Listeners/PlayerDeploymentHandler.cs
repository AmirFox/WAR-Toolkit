using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using WarToolkit.Pathfinding;

public class PlayerDeploymentHandler
{
    private IDeployable _selectedDeployable;
    private ITurnManager _turnManager;
    private ITileQuery _tileQuery;
    private IEventManager _eventManager;
    private MapController _mapController;

    private IPlayer _player;

    public PlayerDeploymentHandler(IPlayer player, ITurnManager turnManager, ITileQuery tileQuery, IEventManager eventManager)
    {
        this._player = player;
        this._turnManager = turnManager;
        this._tileQuery = tileQuery; 
        this._eventManager = eventManager;
    }

    public void StartListening(IPlayer player)
    {
        this._player = player;
        _eventManager.StartListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
        _eventManager.StartListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
    }

    public void StopListening()
    {
        this._player = null;
        _eventManager.StopListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
        _eventManager.StopListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
    }

    private void OnTileSelected(IArguements args)
    {
        if(args is SelectionEventArgs<ITile> selectedTileArgs)
        {
            if(_selectedDeployable != null)
            {
                _turnManager.CurrentPlayer.Deploy(_selectedDeployable, selectedTileArgs.Selection);
                _mapController.ClearHighlights();
            }
        }
    }

    private void OnDeployableSelected(IArguements args)
    {
        if(args is SelectionEventArgs<IDeployable> selectionArgs)
        {
            if(!selectionArgs.Selection.IsDeployed)
            {
                _selectedDeployable = selectionArgs.Selection;
                SetSelection();
            }
        }
    }

        private void SetSelection()
        {
            List<Vector2> tiles = _tileQuery.QueryRadius(_player.SpawnZonePosition, _player.spawnZoneSize, true);
            _mapController.HighlightTiles(tiles.ToArray());
        }

    private List<ITile> GetSpawnTiles()
    {
        List<Vector2> spawnTiles = new List<Vector2>();
        
        spawnTiles = _tileQuery.QueryRadius(_player.SpawnZonePosition, _player.spawnZoneSize, true);
        
        return spawnTiles?.Select(t=>_mapController.GetTileAtCoord(t)).ToList() ?? new List<ITile>();
    }
}

