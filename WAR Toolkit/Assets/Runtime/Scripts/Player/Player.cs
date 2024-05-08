using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.Pathfinding;
using Zenject;

namespace WarToolkit.ObjectData
{
    public class Player : IPlayer
    {
        public class Factory : PlaceholderFactory<IFactionData, int, Vector2, int, Player> { }

        private IDeployable _selectedDeployable;
        [Zenject.Inject] private IMapController _mapController;
        [Zenject.Inject] private ITileQuery _tileQuery;
        [Zenject.Inject] private IEventManager _eventManager;
        private List<IUnit> _units;
        public IFactionData factionData { get; }

        public int PlayerIndex { get; }
        public int Resources { get; private set; }

        public Vector2 SpawnZonePosition { get; private set; }

        public int spawnZoneSize { get; private set; }

        public bool IsEliminated {  get => _units.Count < 1 && Resources <= 0; }

        public Player(IFactionData factionData, int resources, Vector2 spawnZonePosition, int spawnZoneSize,
        IMapController mapController, ITileQuery tileQuery, IEventManager eventManager)
        {
            _mapController = mapController;
            _tileQuery = tileQuery;
            _eventManager = eventManager;

            this.factionData = factionData;
            this.Resources = resources;
            this.SpawnZonePosition = spawnZonePosition;
            this.spawnZoneSize = spawnZoneSize;
            _eventManager.StartListening(Constants.EventNames.GAME_STATE_CHANGED, OnStateChanged);
        }

        
        private void OnStateChanged(IArguements args)
        {
            if(args is GameStateChangeArgs stateChangeArgs)
            {
                ClearListeners();
                if(stateChangeArgs.NewPlayerIndex != this.PlayerIndex)
                {
                    return;
                }

                switch(stateChangeArgs.NewPhase)
                {
                    case Core.Enums.Phase.DEPLOYMENT:
                        _eventManager.StartListening(Constants.EventNames.TILE_SELECTED, OnDeploymentTileSelected);
                        _eventManager.StartListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
                        break;
                    case Core.Enums.Phase.MOVEMENT:
                        break;
                    case Core.Enums.Phase.COMBAT:
                        break;
                }
            }
        }

        private void ClearListeners() 
        {
            _eventManager.StopListening(Constants.EventNames.TILE_SELECTED, OnDeploymentTileSelected);
            _eventManager.StopListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
        }
        
        private void OnDeploymentTileSelected(IArguements args)
        {
            if(args is TileSelectedEventArgs selectedTileArgs)
            {
                if(_selectedDeployable != null)
                {
                    Debug.Log("TILE SELECTED!");
                    _selectedDeployable.Deploy(selectedTileArgs.position);
                    Resources -= _selectedDeployable.Cost;
                    _mapController.ClearHighlights();
                }
            }
        }

        private void OnDeployableSelected(IArguements args)
        {
            if(args is DeployableSelectedEventArgs selectionArgs)
            {
                Debug.Log($"[Player {this.PlayerIndex}] DEPLOYABLE SELECTED: {selectionArgs.deployableIndex}");
                if(factionData.TryGetDeployable(selectionArgs.deployableIndex, out _selectedDeployable))
                {
                }
            }
        }

        private List<ITile> GetSpawnTiles()
        {
            List<Vector2> spawnTiles = new List<Vector2>();
        
            spawnTiles = _tileQuery.QueryRadius(SpawnZonePosition, spawnZoneSize, true);
        
            return spawnTiles?.Select(t=>_mapController.GetTileAtCoord(t)).ToList() ?? new List<ITile>();
        }

    }
}
