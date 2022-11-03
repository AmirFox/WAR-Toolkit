namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface defining static map data used to generate tile map levels.
    /// </summary>
    public interface IMapData<T> where T : ITile
    {
        /// <summary>
        /// Width of defined map.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of defined map.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Seed used to randomise generated map information.
        /// </summary>
        string Seed { get; }

        /// <summary>
        /// Retrieves tile data for a given map location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns tile at given x,y co-ordinate or null.</returns>
        T GetTileData(int x, int y);
    }
}