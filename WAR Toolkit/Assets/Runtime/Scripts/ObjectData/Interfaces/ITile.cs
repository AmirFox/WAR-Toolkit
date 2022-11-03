    using UnityEngine;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for tile properties.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// x and y co-ordinate of tile.
        /// </summary>
        Vector2 Coordinates{ get; }

        /// <summary>
        /// Positions of neighbouring tiles.
        /// </summary>
    	Vector2[] Neighbors { get; }

        /// <summary>
        /// Point cost to move to tile.
        /// </summary>
        double BaseMoveValue { get; }

        /// <summary>
        /// Whether the given tile is accessible or not.
        /// </summary>
        bool IsAccesible { get; }
    }
}