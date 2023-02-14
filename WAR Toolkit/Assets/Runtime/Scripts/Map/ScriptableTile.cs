using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.ObjectData;

[CreateAssetMenu]
public class ScriptableTile : RuleTile<ScriptableTile.Neighbor> , ITile
{
    [field: SerializeField] public RuleTile overlayTile;

    #region UI FIELDS
    [field: SerializeField] public double BaseMoveValue { get; private set; } = 1;
    [field: SerializeField] public double BaseDefenceModifier { get; private set; } = 0;
    [field: SerializeField] public bool IsAccesible { get; private set; } = true;
    public Vector2 Coordinates { get => new Vector2(); }
    public Vector2[] Neighbors {get => this.neighborPositions.Cast<Vector2>().ToArray(); }
    #endregion

    public void SetHighlight(bool highlight)
    {

    }

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Null = 3;
        public const int NotNull = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        switch (neighbor) {
            case Neighbor.Null: return tile == null;
            case Neighbor.NotNull: return tile != null;
        }
        return base.RuleMatch(neighbor, tile);
    }
}