using UnityEngine;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// A basic implementation of tile properties on a MonoBehaviour.
    /// </summary>
    public class Tile : MonoBehaviour, ITile
    {
        #region PUBLIC FIELDS
        #region UI FIELDS
        [field: SerializeField] public double BaseMoveValue { get; private set; } = 1;
        [field: SerializeField] public double BaseDefenceModifier { get; private set; } = 0;
        [field: SerializeField] public bool IsAccesible { get; private set; } = true;
        #endregion

        public Vector2 Coordinates { get; private set; }
        public Vector2[] Neighbors { get; private set; } = new Vector2[4];
        #endregion

        #region PUBLIC METHODS
        public void Initialize(int x, int y)
        {
            this.Initialize(new Vector2(x, y));
        }

        public void Initialize(Vector2 coord)
        {
            this.Coordinates = coord;
        }
        #endregion
    }
}