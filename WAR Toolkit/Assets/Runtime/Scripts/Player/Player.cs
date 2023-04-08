using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public class Player : IPlayer
    {
        private List<GameObject> _units;
        public FactionData factionData { get; }

        public int PlayerIndex { get; }
        public int Resources { get; private set; }

        public bool IsEliminated {  get => _units.Count < 1 && Resources <= 0; }

        public Player(FactionData factionData)
        {
            this.factionData = factionData;
        }

        public void Deploy(IDeployable unit, ITile tile)
        {
            if(factionData.Deployables.Contains(unit))
            {
                if(unit.Cost <= Resources)
                {
                    unit.Deploy(tile);
                    Resources -= unit.Cost;
                }
            }
        }
    }
}
