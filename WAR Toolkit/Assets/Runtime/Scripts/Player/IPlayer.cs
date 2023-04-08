using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public interface IPlayer
    {
        FactionData factionData { get; }
        int PlayerIndex { get; }
        int Resources { get; }
        bool IsEliminated { get; }

        void Deploy(IDeployable unit, ITile tile);
    }
}