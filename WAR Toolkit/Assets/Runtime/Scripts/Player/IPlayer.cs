using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public interface IPlayer
    {
        IFactionData factionData { get; }
        int PlayerIndex { get; }
        int Resources { get; }
        bool IsEliminated { get; }
        Vector2 SpawnZonePosition { get; }
        int spawnZoneSize { get; }

        void Deploy(IDeployable unit, ITile tile);
    }
}