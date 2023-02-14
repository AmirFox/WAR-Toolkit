using UnityEngine;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Component interface for highlighting objects.
    /// </summary>
    public interface IHighlight
    {
        /// <summary>
        /// Highlight an object with the given color.
        /// </summary>
        /// <param name="color">Color for target highlight</param>
        void Show(Color color);

        /// <summary>
        /// Resets highlight to original color.
        /// </summary>
        void Clear();
    }
}