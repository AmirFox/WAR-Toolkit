using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using Zenject;
using static Constants;
using System.Collections.Generic;
using System.Linq;
using WarToolkit.Pathfinding;

public class MapController : MonoBehaviour, IMapController<DataTile>
{
    #region FIELDS
    [field:SerializeField]
    private Tilemap _tileMap;

    [field:SerializeField]
    private Tilemap _overlayTiles;

    [Inject]
    private IMapData<DataTile> _mapData;

    [Inject]
    private IEventManager _eventManager;

    [Inject]
    private ITileQuery<DataTile> _query;
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

    public void GenerateMap()
    {
        for(int x = 0; x < _mapData.Width; x++)
        {
            for(int y = 0; y < _mapData.Height; y++)
            {
                DataTile tile = _mapData.GetTileData(x,y);
                _tileMap.SetTile(new Vector3Int(x,y), tile as TileBase);
                if(tile.OverlayTile != null)
                {
                    _overlayTiles.SetTile(new Vector3Int(x,y), tile.OverlayTile);
                }
            }
        }
    }

    public DataTile GetTileAtCoord(Vector2 coordinate)
    {
        Vector3Int cell = new Vector3Int((int)coordinate.x, (int)coordinate.y);
        return this.GetTile(cell);
    }

    public DataTile[] GetNeighbors(DataTile tile)
    {
        HashSet<Vector3Int> neighborPositions = tile.neighborPositions;
        return neighborPositions.Select(p=>GetTile(p)).ToArray();
    }

    public DataTile[] GetSpawnPoint(int teamIndex)
    {
        if(teamIndex < _mapData.SpawnPoints.Length)
        {
            Vector2 spawnPoint = _mapData.SpawnPoints[teamIndex];
            DataTile origin = GetTileAtCoord(spawnPoint);

            if(origin == null) return new DataTile[0];
            if(_mapData.SpawnAreaSize < 1) return new DataTile[0];

            _query.QueryRadius(origin, _mapData.SpawnAreaSize);
        }
        return new DataTile[0];
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
