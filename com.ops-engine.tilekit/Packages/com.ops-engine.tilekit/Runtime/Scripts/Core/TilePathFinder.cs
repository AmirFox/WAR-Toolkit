using UnityEngine;
using System.Collections.Generic;
using OpsEngine.Tilekit.Core;
using OpsEngine.Tilekit.ObjectData;

namespace OpsEngine.Tilekit.Core
{
/// <summary>
/// Finds shortest paths between two tiles using djikstras algorithm
/// </summary>
public class TilePathFinder<T> : ITilePathFinder<T> where T : ITile
	{	
		#region PRIVATE MEMBERS
		private ITileMap<T> _tileMap;
		#endregion

		#region CONSTRUCTOR
		public TilePathFinder (ITileMap<T> tileMap) 
		{
			_tileMap = tileMap;
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
				if (current.Last.Coordinates == destination.Coordinates)
				{
					return current;
				}

				//close the last tile of the current path
				closed.Add(current.Last);

				for (int i = 0; i < current.Last.Neighbors.Length; i++)
				{
					Vector2 neighborPosition = current.Last.Neighbors[i];
					if(neighborPosition == null) continue;

					T neighbor = _tileMap.GetTile(neighborPosition);
					if(neighbor == null) continue;

					if (!neighbor.IsAccesible || neighbor.IsOccupied) continue;

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