using System.Collections.Generic;
using WarToolkit.ObjectData;
using WarToolkit.Core.Enums;
using UnityEngine;

namespace WarToolkit.Pathfinding
{
	/// <summary>
	/// Interface higlighting tiles on a map.
	/// </summary>
    public interface ITileQuery
    {
		/// <summary>
		/// Gets a list of highlighted tiles about an origin point.
		/// </summary>
		/// <param name="origin">Origin point of highlight area</param>
		/// <param name="points">Points defining maximum higlight distance</param>
		/// <param name="includeOrigin">Whether to include origin in results</param>
		/// <returns>Returns all tiles around origin within distance threshold.</returns>
        List<Vector2> QueryRadius(Vector2 origin, int points, bool includeOrigin = false);
    }
}
