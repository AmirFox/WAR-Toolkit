using UnityEngine;
using System.Collections.Generic;
using WarToolkit.Core;
using WarToolkit.ObjectData;

namespace WarToolkit.Pathfinding
{
/// <summary>
/// Finds shortest paths between two tiles using djikstras algorithm
/// </summary>
public class MovementPathfinder : ITilePathFinder
	{	
		#region PRIVATE MEMBERS
		protected readonly IMapController _mapController;
		#endregion

		#region CONSTRUCTOR
		public MovementPathfinder (IMapController mapController) 
		{
			_mapController = mapController;
		}
		#endregion

		#region PUBLIC METHODS
		public virtual TilePath FindPath(Vector2 origin, Vector2 destination) 
		{
			//use open and closed list of tiles / paths
			var closed = new List<Vector2>();
			var open = new Queue<TilePath>();

			//add the original tile to open list
			TilePath originPath = new TilePath();
			ITile originTileData = _mapController.GetTileAtCoord(origin); 
			
			originPath.AddTile(origin, originTileData.BaseMoveValue);
			open.Enqueue(originPath);

			//while there are still open paths - 
			while (open.TryDequeue(out TilePath current)) 
			{
				//if the last tile of current path is closed - move to the next path
				if (closed.Contains(current.Last)) continue;
				
				//if the last tile is the destination - this is the shortest path - return it
				if (current.Last.Equals(destination))
				{
					return current;
				}

				//close the last tile of the current path
				closed.Add(current.Last);

				Vector2[] neighbors = _mapController.GetNeighbors(current.Last);
				for (int i = 0; i < neighbors.Length; i++)
				{
					Vector2 neighbor = neighbors[i];
					if(neighbor == null) continue;
                    
					ITile neighborData = _mapController.GetTileAtCoord(neighbor);

					if (!neighborData.IsAccesible) continue;

					TilePath newPath = new TilePath(current);
					newPath.AddTile(neighbor, neighborData.BaseMoveValue);
					open.Enqueue(newPath);
				}
			}
			return null;
		}
		#endregion
	}
}