using UnityEngine;

namespace OpsEngine.Tilekit.ObjectData
{
    /// <summary>
    /// A basic implementation of tile properties on a MonoBehaviour.
    /// </summary>
    public class Tile : MonoBehaviour, ITile
    {
        #region PUBLIC FIELDS
        #region UI FIELDS
        [field: SerializeField] public double BaseMoveValue { get; private set; } = 1;
        [field: SerializeField] public bool IsAccesible { get; private set; } = true;
        [field: SerializeField]
        #endregion

        public Vector2 Coordinates { get; private set; }
        public Vector2[] Neighbors { get; private set; } = new Vector2[4];
        public bool IsOccupied { get; set; } = false;
        public bool IsHighlighted { get; set; }
        #endregion

        #region PUBLIC METHODS
        public void Initialize(int x, int y, bool isOccupied = false)
        {
            this.Initialize(new Vector2(x, y), isOccupied);
        }

        public void Initialize(Vector2 coord, bool isOccupied = false)
        {
            this.Coordinates = coord;
            this.IsOccupied = isOccupied;
        }
        #endregion
    }
}