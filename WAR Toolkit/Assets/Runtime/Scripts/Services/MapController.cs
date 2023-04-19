using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using Zenject;
using static Constants;
using System.Collections.Generic;
using System.Linq;
using WarToolkit.Pathfinding;

public class MapController : MonoBehaviour, IMapController
{
    #region FIELDS
    [field:SerializeField]
    private Tilemap _tileMap;

    [field:SerializeField]
    private Tilemap _overlayTiles;

    [field:SerializeField]
    private Tilemap _highlightLayer;

    [Inject]
    private MatchData matchData;

    [Inject]
    private IEventManager _eventManager;

    [Inject]
    private ITileQuery _query;

    private MapData _mapData => matchData.mapData;
    #endregion

    private void Start() 
    {
        this.GenerateMap();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 cameraPosition = Camera.main.transform.localPosition;
            float mouseZ = Camera.main.WorldToScreenPoint(new Vector3(0,0,Input.mousePosition.z)).z;
            Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouseZ));

            Vector3 direction = new Vector3(0,0,cameraPosition.z);
            
            if(Physics.Raycast(origin, direction, out RaycastHit _, 20))
            {
                Debug.DrawRay(origin, direction, Color.red, 15f);
            }

            var hit = Physics2D.Raycast(new Vector2(origin.x, origin.y), Vector2.up);
            if (hit.collider != null && hit.collider.gameObject == this._tileMap.gameObject) 
            {
                Vector3 point = hit.point;
                Vector3Int cell = this._tileMap.WorldToCell(point);
                DataTile selection = _tileMap.GetTile<DataTile>(cell);
                if(selection != null)
                {
                    this._eventManager.TriggerEvent(EventNames.TILE_SELECTED, new SelectionEventArgs<ITile>(selection));
                }
            }
        }
    }
    #region 
    public void HighlightTiles(Vector2[] positions)
    {
        foreach(Vector2 position in positions)
        {
            Debug.Log($"Highlighting tile at {position}");
            _highlightLayer.SetTile(new Vector3Int((int)position.x, (int)position.y), _mapData.highlightTile);
        }
    }

    public void ClearHighlights()
    {
        _highlightLayer.ClearAllTiles();
    }

    public void GenerateMap()
    {
        for(int x = 0; x < _mapData.Width; x++)
        {
            for(int y = 0; y < _mapData.Height; y++)
            {
                DataTile tile = _mapData.GetTileData(x,y);
                _tileMap.SetTile(new Vector3Int(x,y), tile);
                
                RuleTile overlayTile = _mapData.GetOverlayTile(x,y);
                if(overlayTile != null)
                {
                    _overlayTiles.SetTile(new Vector3Int(x,y), overlayTile);
                }
            }
        }
    }

    public ITile GetTileAtCoord(Vector2 coordinate)
    {
        Vector3Int cell = new Vector3Int((int)coordinate.x, (int)coordinate.y);
        return this.GetTile(cell);
    }

    public Vector2[] GetNeighbors(Vector2 position)
    {
        List<Vector2> neighbors = new List<Vector2>();

        // Check left neighbor
        if (position.x > 0)
            neighbors.Add(new Vector2(position.x - 1, position.y));

        // Check right neighbor
        if (position.x < _mapData.Width - 1)
            neighbors.Add(new Vector2(position.x + 1, position.y));

        // Check bottom neighbor
        if (position.y > 0)
            neighbors.Add(new Vector2(position.x, position.y - 1));

        // Check top neighbor
        if (position.y < _mapData.Height - 1)
            neighbors.Add(new Vector2(position.x, position.y + 1));

        return neighbors.ToArray();
    }

    public Vector2[] GetSpawnArea(int teamIndex)
    {
        // if(teamIndex < _mapData.SpawnPoints.Length)
        // {
        //     Vector2 spawnPoint = _mapData.SpawnPoints[teamIndex];

        //     if(spawnPoint == null) return new Vector2[0];
        //     if(_mapData.SpawnAreaSize < 1) return new Vector2[0];
            
        //     return _query.QueryRadius(spawnPoint, _mapData.SpawnAreaSize).ToArray();
        // }
        return new Vector2[0];
    }

    public void Clear()
    {
        _tileMap.ClearAllTiles();
    }
    #endregion

    #region PRIVATE METHODS
    private DataTile GetTile(Vector3Int cell)
    {
        return this._tileMap.GetTile<DataTile>(cell);
    }
    #endregion
}
