using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.ObjectData
{
    [CreateAssetMenu(fileName = "Team Data", menuName = "Custom Assets/Team Data")]
    public class FactionData : ScriptableObject, IFactionData<Unit>
    {
        [field: SerializeField]
        private List<Unit> _unitTypes;

        public List<string> GetUnitTypeIdentifiers()
        {
            return _unitTypes.Select(u=>u.typeIdentifier).ToList();
        }

        public int GetUnitCost(string typeIdentifier)
        {
            return _unitTypes.FirstOrDefault(u=>string.Equals(u.typeIdentifier, typeIdentifier))?.spawnCost ?? -1;
        }

        public Unit GetUnit(string typeIdentifier)
        {
            Unit unit = _unitTypes.FirstOrDefault(u=>string.Equals(u.typeIdentifier, typeIdentifier));
            if(unit != null)
            {
                return Unit.Instantiate(unit);
            }

            return null;
        }
    }
}
