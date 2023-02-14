using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.ObjectData;

[CreateAssetMenu]
public class InteractableTile : UnityEngine.Tilemaps.Tile, ITile
{
        #region PUBLIC FIELDS
        #region UI FIELDS
        [field: SerializeField] public double BaseMoveValue { get; private set; } = 1;
        [field: SerializeField] public double BaseDefenceModifier { get; private set; } = 0;
        [field: SerializeField] public bool IsAccesible { get; private set; } = true;
        #endregion

        public Vector2 Coordinates { get; private set; }
        public Vector2[] Neighbors { get; private set; } = new Vector2[4];

        public IUnit Occupier { get; set; }
        #endregion

    public void SetHighlight(bool highlight)
    {
        throw new System.NotImplementedException();
    }
}
