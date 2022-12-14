using UnityEngine;

namespace WarToolkit.ObjectData
{
	/// <summary>
	/// Interface used to generate and store map of tiles.
	/// </summary>
	/// <typeparam name="T">Type of tile to use in map.</typeparam>
    public interface ITileMap<T> where T : ITile
    {
		/// <summary>
		/// Generates a tile map into the game.
		/// </summary>
        void GenerateMap();

        /// <summary>
        /// Retrieves a tile at the given position.
        /// </summary>
        /// <param name="x">x co-ordinate of tile</param>
        /// <param name="y">y co-ordinate of tile</param>
        /// <returns>Returns tile at given point or null.</returns>
        T GetTile(Vector2 coordinate);

		/// <summary>
		/// Gets all tiles in the map.
		/// </summary>
		/// <returns>Returns a 2D array of all tiles in map.</returns>
        T[,] GetAllTiles();

		/// <summary>
		/// Clears all tiles from map.
		/// </summary>
        void Clear();
    }
}