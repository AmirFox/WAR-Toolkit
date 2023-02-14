using System.Collections.Generic;
using System.Linq;
using WarToolkit.ObjectData;

namespace WarToolkit.Pathfinding
{
    /// <summary>
    /// A path of tiles used in path highlighting and movement.
    /// </summary>
    public class TilePath<T> where T : ITile
    {
        #region PROPERTIES    
        public T Last { get; private set; }
        public double Cost { get; private set; }
        #endregion

        #region PRIVATE MEMBERS
        private IList<T> _tiles;
        #endregion

        #region CONSTRUCTORS
        public TilePath() 
        {
            _tiles = new List<T>();
            Cost = 0;
            Last = default(T);
        }

        public TilePath(TilePath<T> tp)
        {
            _tiles = tp._tiles.ToList();
            Cost = tp.Cost;
            Last = tp.Last;
        }
        #endregion

        #region PUBLIC METHODS
        public IEnumerable<T> PathToTiles()
        {
            _tiles.Distinct();
            _tiles.Remove(_tiles[0]);
            return _tiles;
        }

        public void AddTile(T t)
        {
            Cost += t.BaseMoveValue;

            _tiles.Add(t);
            Last = t;
        }
    	#endregion
    }
}