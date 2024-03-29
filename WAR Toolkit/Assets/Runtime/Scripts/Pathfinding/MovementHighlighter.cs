﻿using UnityEngine;
using System.Collections.Generic;
using WarToolkit.ObjectData;
using WarToolkit.Core.Enums;
using Zenject;

namespace WarToolkit.Pathfinding
{
    /// <summary>
    /// Interface for a utility class used to highlight tiles on a map.
    /// </summary>
    public class MovementHighlighter : ITileQuery
    {
        #region PROTECTED FIELDS
        [Inject]
        protected IMapController _mapController;
        #endregion

        #region CTORS
        public MovementHighlighter(IMapController mapController)
        {
            _mapController = mapController;
        }
        #endregion

        #region PUBLIC METHODS
        public virtual List<Vector2> QueryRadius(Vector2 origin, int points, bool includeOrigin = false)
        {
            //open list of paths and closed list of tiles
            List<Vector2> closed = new List<Vector2>();
            Queue<TilePath> open = new Queue<TilePath>();

            //path going out from origin
            TilePath originPath = new TilePath();
            originPath.AddTile(origin,  _mapController.GetTileAtCoord(origin).BaseMoveValue);
            open.Enqueue(originPath);

            //while there are paths still in the open list (unchecked paths) - 
            while (open.TryDequeue(out TilePath current))
            {
                //if the closed list contains the last tile of the current path - look at the next path
                if (closed.Contains(current.Last))
                    continue;

                //or if the path costs more than the available movement points - look at the next path
                if (current.Cost > (double)points + 1)
                    continue;

                //add the last tile of the current path to the closed list
                closed.Add(current.Last);

                Vector2[] neighbors = _mapController.GetNeighbors(current.Last);
                //for all tiles neighbouring the last tile (if they are unoccupied) - add them to the open path list
                for (int i = 0; i < neighbors.Length; i++)
                {
                    Vector2 neighbor = neighbors[i];

                    if (neighbor == null) continue;

                    ITile neighborData = _mapController.GetTileAtCoord(neighbor);

                    if (!neighborData.IsAccesible) continue;

                    TilePath newPath = new TilePath(current);
                    newPath.AddTile(neighbor, neighborData.BaseMoveValue);
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

        #region PRIVATE METHODS
        private double Get(Vector2 position)
        {
            return _mapController.GetTileAtCoord(position)?.BaseMoveValue ?? 0;
        }
        #endregion
    }
}
