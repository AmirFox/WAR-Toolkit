using UnityEngine;
using System.Collections.Generic;
using WarToolkit.Core;
using WarToolkit.ObjectData;

namespace WarToolkit.Pathfinding
{
/// <summary>
/// Finds shortest paths between two tiles using djikstras algorithm
/// </summary>
public class MovementPathfinder<T> : ITilePathFinder<T> where T : ITile
	{	
		#region PRIVATE MEMBERS
		protected readonly IMapController<T> _mapController;
		#endregion

		#region CONSTRUCTOR
		public MovementPathfinder (IMapController<T> mapController) 
		{
			_mapController = mapController;
		}
		#endregion

		#region PUBLIC METHODS
		public virtual TilePath<T> FindPath(T origin, T destination) 
		{
			//use open and closed list of tiles / paths
			var closed = new List<T>();
			var open = new Queue<TilePath<T>>();

			//add the original tile to open list
			TilePath<T> originPath = new TilePath<T>();
			originPath.AddTile(origin);
			open.Enqueue(originPath);

			//while there are still open paths - 
			while (open.TryDequeue(out TilePath<T> current)) 
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

				T[] neighbors = _mapController.GetNeighbors(current.Last);
				for (int i = 0; i < neighbors.Length; i++)
				{
					T neighbor = neighbors[i];
					if(neighbor == null) continue;

					if (!neighbor.IsAccesible) continue;

					TilePath<T> newPath = new TilePath<T>(current);
					newPath.AddTile(neighbor);
					open.Enqueue(newPath);
				}
			}
			return null;
		}
		#endregion
	}
}