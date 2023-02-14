using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.ObjectData
{
    public interface IFactionData
    {
        IDeployable[] Deployables { get; }
    }

    [CreateAssetMenu(fileName = "Team Data", menuName = "Custom Assets/Team Data")]
    public class FactionData : ScriptableObject, IFactionData
    {
        [field: SerializeField]
        private Deployable[] _deployables;

        [field: SerializeField]
        public string FactionName { get; private set; }

        public IDeployable[] Deployables { get => _deployables; }
    }
}
