using System.Collections.Generic;
using System.Linq;
using OpsEngine.Tilekit.ObjectData;
using OpsEngine.Tilekit.Core;
using UnityEngine;

namespace OpsEngine.Tilekit.Controllers
{
    /// <summary>
    /// Unity component wrapping functionality for creating maps, pathfinding and path highlighting
    /// </summary>
    public class MapController : MonoBehaviour
    {
        [Tooltip("Static data defining properties of the map being generated.")]
        [field: SerializeField] 
        private MapData _mapData;
        
        private MovementHighlighter<Tile> _tileHighlighter;
        private MovementPathfinder<Tile> _tilePathFinder;
        private ITileMap<Tile> _tileMap;

        private GameObject _mapRoot;

        private void Awake()
        {
            _tileMap = new TileMap<Tile>(_mapData);
            _tileHighlighter = new MovementHighlighter<Tile>(_tileMap);
            _tilePathFinder = new MovementPathfinder<Tile>(_tileMap);
        }

        /// <summary>
        /// Generate a map game object with children for each tile.
        /// </summary>
        public void CreateMap()
        {
            if(_mapRoot == null) { _mapRoot = new GameObject("Map"); }
            else { ClearMap(); }
            
            _tileMap.GenerateMap();
            foreach (ITile tile in _tileMap.GetAllTiles())
            {
                if (tile is Tile tileInstance)
                {
                    tileInstance.transform.parent = _mapRoot.transform;
                }
            }
        }

        /// <summary>
        /// Clears all map data from the scene.
        /// </summary>
        public void ClearMap()
        {
            foreach (Transform child in _mapRoot.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            _tileMap.Clear();
        }

        /// <summary>
        /// Gets a set of tiles to highlight around an origin point.
        /// </summary>
        /// <param name="origin">Center of highlight area</param>
        /// <param name="points">Distance of highlight area</param>
        /// <returns>Returns tiles to be highlighted.</returns>
        public IEnumerable<Tile> FindHighlight(Vector2 origin, int points)
        {
            Tile tile = _tileMap.GetTile(origin);
            return _tileHighlighter.FindHighlight(tile, points);
        }

        /// <summary>
        /// Gets a set of consecutive tiles forming a path between two points.
        /// </summary>
        /// <param name="origin">Origin point of path</param>
        /// <param name="destination">Destination point of path</param>
        /// <returns>Returns tiles forming path between origin and destination.</returns>
        public IEnumerable<Tile> FindPath(Vector2 origin, Vector2 destination)
        {
            Tile originTile = _tileMap.GetTile(origin);
            Tile destinationTile = _tileMap.GetTile(destination);

            return _tilePathFinder.FindPath(originTile, destinationTile).PathToTiles();
        }

        /// <summary>
        /// Get all tile game objects within a map.
        /// </summary>
        /// <returns>Returns all tiles within the map.</returns>
        public IEnumerable<Tile> GetAll()
        {
            Tile[,] tiles = _tileMap.GetAllTiles();
            IEnumerable<ITile> toEnumerable = tiles.Cast<ITile>();
            return toEnumerable.Select(t => t as Tile);
        }
    }
}