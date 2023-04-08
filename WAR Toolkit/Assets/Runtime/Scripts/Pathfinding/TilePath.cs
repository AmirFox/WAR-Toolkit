using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.Pathfinding
{
    /// <summary>
    /// A path of tiles used in path highlighting and movement.
    /// </summary>
    public class TilePath
    {
        #region PROPERTIES    
        public Vector2 Last { get; private set; }
        public double Cost { get; private set; }
        #endregion

        #region PRIVATE MEMBERS
        private IList<Vector2> _positions;
        #endregion

        #region CONSTRUCTORS
        public TilePath() 
        {
            _positions = new List<Vector2>();
            Cost = 0;
            Last = default;
        }

        public TilePath(TilePath tp)
        {
            _positions = tp._positions.ToList();
            Cost = tp.Cost;
            Last = tp.Last;
        }
        #endregion

        #region PUBLIC METHODS
        public void AddTile(Vector2 position, double cost)
        {
            Cost += cost;

            _positions.Add(position);
            Last = position;
        }
    	#endregion
    }
}