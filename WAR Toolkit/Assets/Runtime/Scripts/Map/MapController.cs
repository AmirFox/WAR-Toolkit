using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.ObjectData;
using Zenject;

public class MapController : MonoBehaviour, IMapController<DataTile>
{
    #region FIELDS
    [field:SerializeField]
    private Tilemap _tileMap;

    [field:SerializeField]
    private Tilemap _overlayTiles;

    [Inject]
    private IMapData<DataTile> _mapData;
    #endregion

    private void Start() 
    {
        this.GenerateMap();
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
                if(tile.overlayTile != null)
                {
                    _overlayTiles.SetTile(new Vector3Int(x,y), tile.overlayTile);
                }
            }
        }
    }

    public DataTile GetTile(Vector2 coordinate)
    {
        return this._tileMap.GetTile<DataTile>(new Vector3Int((int)coordinate.x, (int)coordinate.y));
    }

    public void Clear()
    {
        _tileMap.ClearAllTiles();
    }
    #endregion
}
