using UnityEngine;

namespace WarToolkit.ObjectData
{
	/// <summary>
	/// Interface used to generate and store map of tiles.
	/// </summary>
	/// <typeparam name="T">Type of tile to use in map.</typeparam>
    public interface IMapController<T> where T : ITile
    {
		/// <summary>
		/// Generates a tile map into the game.
		/// </summary>
        void GenerateMap();

        /// <summary>
        /// Retrieves a tile at the given 2D coordinate.
        /// </summary>
        /// <param name="x">x co-ordinate of tile</param>
        /// <param name="y">y co-ordinate of tile</param>
        /// <returns>Returns tile at given 2D coordinate or null.</returns>
        T GetTileAtCoord(Vector2 coordinate);

        /// <summary>
        /// Get an array of neighboring tiles for the given tile
        /// </summary>
        /// <param name="tile">the tile to find neighbors for</param>
        /// <returns>An array of all neighboring tiles</returns>
        T[] GetNeighbors(T tile);

		/// <summary>
		/// Clears all tiles from map.
		/// </summary>
        void Clear();
    }
}