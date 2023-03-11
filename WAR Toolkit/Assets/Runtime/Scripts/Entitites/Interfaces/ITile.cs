    using UnityEngine;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for tile properties.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Display name to show for this tile 
        /// </summary>
        /// <value></value>
        public string DisplayName { get; }

        /// <summary>
        /// Point cost to move to tile.
        /// </summary>
        double BaseMoveValue { get; }

        /// <summary>
        /// Whether the given tile is accessible or not.
        /// </summary>
        bool IsAccesible { get; }

        /// <summary>
        /// Modifier for a unit's defence value on tile.
        /// </summary>
        /// <value></value>
        double BaseDefenceModifier { get; }

        public void SetHighlight(bool highlight);
    }
}