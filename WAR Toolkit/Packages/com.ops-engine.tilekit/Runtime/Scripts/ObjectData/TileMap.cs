using UnityEngine;

namespace OpsEngine.Tilekit.ObjectData
{
	/// <summary>
	/// Core class used to generated and store map of tiles.
	/// </summary>
	/// <typeparam name="T">Type of tile to use in map.</typeparam>
    public class TileMap<T> : ITileMap<T> where T : ITile
    {
        #region PRIVATE MEMBERS
        private IMapData<T> _mapData;
        private T[,] _map;
        #endregion

        #region CTOR
        public TileMap(IMapData<T> mapData)
        {
            _mapData = mapData;
        }
        #endregion

        #region  I_TILEMAP METHODS
        public void GenerateMap()
        {
            Clear();

            //for each tile in the text file
            for (int y = 0; y < _mapData.Height; y++)
            {
                for (int x = 0; x < _mapData.Width; x++)
                {
                    _map[x, y] = _mapData.GetTileData(x, y);

                    //up
                    if (y > 0)
                    {
                        Vector2 n = new Vector2(x, y - 1);
                        _map[x, y].Neighbors[0] = new Vector2((int)n.x, (int)n.y);
                    }
                    //down
                    if (y < _mapData.Height - 1)
                    {
                        Vector2 n = new Vector2(x, y + 1);
                        _map[x, y].Neighbors[1] = new Vector2((int)n.x, (int)n.y);
                    }

                    //left
                    if (x > 0)
                    {
                        Vector2 n = new Vector2(x - 1, y);
                        _map[x, y].Neighbors[2] = new Vector2((int)n.x, (int)n.y);
                    }
                    //right
                    if (x < _mapData.Width - 1)
                    {
                        Vector2 n = new Vector2(x + 1, y);
                        _map[x, y].Neighbors[3] = new Vector2((int)n.x, (int)n.y);
                    }
                }
            }
        }

        public T GetTile(Vector2 coordinates)
        {
            if (_map == null) { return default(T); }
            if (coordinates.x > _mapData.Height) { return default(T); }
            if (coordinates.y > _mapData.Width) { return default(T); }

            return _map[(int)coordinates.x, (int)coordinates.y];
        }

        public void Clear()
        {
            _map = new T[_mapData.Width, _mapData.Height];
        }

        public T[,] GetAllTiles()
        {
            return _map;
        }
        #endregion
    }
}
