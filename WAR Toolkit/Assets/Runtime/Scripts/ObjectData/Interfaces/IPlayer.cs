using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public interface IPlayer
    {
        public int Resources { get; }

        public bool IsEliminated { get; }

        public bool CanDeploy();

        public void ResetState();
    }
}