using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public class Player<T> : IPlayer where T : IUnit
    {
        private IFactionData<T> _factionData;
        private List<T> _deployedUnits = new List<T>();

        public int Resources { get; private set; }
        
        public bool IsEliminated  { get => _deployedUnits.Count == 0 && Resources <= 0; }

        public Player(IFactionData<T> factionData)
        {
            _factionData = factionData;
        }

        public void ResetState()
        {
            foreach(IUnit unit in _deployedUnits)
            {
                unit.ResetState();
            }
        }

        public void SpawnUnit(string typeIdentifier, ITile tile)
        {
            int unitCost = _factionData.GetUnitCost(typeIdentifier); 
            if(unitCost < 0)
            {
                Debug.LogError("Could not find unit of type.");
            }
            else if(unitCost > Resources)
            {
                Debug.LogWarning("Not enough resources to spawn unit");
            }
            else
            {
                T unit = _factionData.GetUnit(typeIdentifier);
                _deployedUnits.Add(unit);
            }
        }
                
        public bool CanDeploy()
        {
            foreach (string typeIdentifier in _factionData.GetUnitTypeIdentifiers())
            {
                if (_factionData.GetUnitCost(typeIdentifier) <= Resources)
                    return true;
            }
            
            return false;
        }
    }
}
