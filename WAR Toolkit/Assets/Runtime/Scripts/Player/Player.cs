using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public class Player : IPlayer
    {
        private FactionData _factionData;
        private List<GameObject> _units;
        public int PlayerIndex { get; }
        public int Resources { get; private set; }

        public bool IsEliminated {  get => _units.Count < 1 && Resources <= 0; }

        public Player(FactionData factionData)
        {
            _factionData = factionData;
        }
    }
}
