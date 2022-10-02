using UnityEngine;

namespace OpsEngine.Tilekit.ObjectData
{
    /// <summary>
    /// Component interface for highlighting tiles.
    /// </summary>
    public interface IHighlight
    {
        /// <summary>
        /// Highlight a tile with the given color.
        /// </summary>
        /// <param name="color">Color for target highlight</param>
        void Show(Color color);

        /// <summary>
        /// Resets highlight to original color.
        /// </summary>
        void Clear();
    }
}