using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.ObjectData
{
    public interface IFactionData
    {
        int RosterSize { get; }
        bool TryGetDeployable(int index, out IDeployable deployable);
    }

    [CreateAssetMenu(fileName = "Team Data", menuName = "Custom Assets/Team Data")]
    public class FactionData : ScriptableObject, IFactionData
    {
        [field: SerializeField]
        private Unit[] _deployables;

        [field: SerializeField]
        public string FactionName { get; private set; }
        
        public int RosterSize => _deployables.Length;

        public bool TryGetDeployable(int index, out IDeployable deployable)
        {
            deployable = null;
            
            if((index >= _deployables.Length) || (index < 0)) return false;

            deployable = _deployables[index];
            
            return true;
        }
    }
}
