using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.Pathfinding
{
	/// <summary>
	/// Finds shortest paths between two tiles.
	/// </summary>
	public interface ITilePathFinder
	{	
		/// <summary>
		/// Finds shortest path between origin and destination tile.
		/// </summary>
		/// <param name="originTile">the starting tile for movement</param>
		/// <param name="destinationTile">Target tile to move to</param>
		/// <returns>List of tiles forming shortest path.</returns>
		TilePath FindPath(Vector2 origin, Vector2 destination);
    }
}