using WarToolkit.ObjectData;
using UnityEngine;

namespace WarToolkit.PlayTests
{
    /// <summary>
    /// A test implementation of a tile with public accessor fields.
    /// </summary>
    public class TestTile : ITile
    {
        #region PUBLIC FIELDS
        public double BaseMoveValue { get; set; }
        public double BaseDefenceModifier { get; set; }
        public bool IsAccesible { get; set; }
        public Vector2 Coordinates { get; set; }
        public Vector2[] Neighbors { get; set; } = new Vector2[4];
        #endregion

        #region CTORS
        public TestTile(int x, int y, double movementCost, double defenceModifier, bool isAccesible) : this(new Vector2(x, y), movementCost, defenceModifier, isAccesible)
        {
        }

        public TestTile(Vector2 coord, double movementCost, double defenceModifier, bool isAccesible)
        {
            this.Coordinates = coord;
            this.IsAccesible = isAccesible;
            this.BaseMoveValue = movementCost;
        }
        #endregion
    }
}