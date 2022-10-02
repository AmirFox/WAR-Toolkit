using UnityEngine;
using System.Collections.Generic;
using OpsEngine.Tilekit.ObjectData;

namespace OpsEngine.Tilekit.Core
{
    /// <summary>
    /// Interface for a utility class used to highlight tiles on a map.
    /// </summary>
    public class TileHighlighter<T> : ITileHighlighter<T> where T : ITile
    {
        #region PRIVATE MEMBERS
        private ITileMap<T> _tileMap;
        #endregion

        #region CTORS
        public TileHighlighter(ITileMap<T> tileMap)
        {
            _tileMap = tileMap;
        }
        #endregion

        #region PUBLIC METHODS
        public virtual List<T> FindHighlight(T origin, int points, bool includeOrigin = false)
        {
            //open list of paths and closed list of tiles
            List<T> closed = new List<T>();
            Queue<TilePath<T>> open = new Queue<TilePath<T>>();

            //path going out from origin
            TilePath<T> originPath = new TilePath<T>();
            originPath.AddTile(origin);
            open.Enqueue(originPath);

            //while there are paths still in the open list (unchecked paths) - 
            while (open.TryDequeue(out TilePath<T> current))
            {
                //if the closed list contains the last tile of the current path - look at the next path
                if (closed.Contains(current.Last))
                    continue;

                //or if the path costs more than the available movement points - look at the next path
                if (current.Cost > (double)points + 1)
                    continue;

                //add the last tile of the current path to the closed list
                closed.Add(current.Last);

                //for all tiles neighbouring the last tile (if they are unoccupied) - add them to the open path list
                for (int i = 0; i < current.Last.Neighbors.Length; i++)
                {
                    Vector2 neighborPosition = current.Last.Neighbors[i];
                    if (neighborPosition == null) continue;

                    T neighbor = _tileMap.GetTile(neighborPosition);

                    if (neighbor == null) continue;

                    if (!neighbor.IsAccesible || neighbor.IsOccupied) continue;

                    TilePath<T> newPath = new TilePath<T>(current);
                    newPath.AddTile(neighbor);
                    open.Enqueue(newPath);
                }
            }

            if (!includeOrigin)
            {
                closed.Remove(origin);
            }

            return closed;
        }
        #endregion
    }
}
