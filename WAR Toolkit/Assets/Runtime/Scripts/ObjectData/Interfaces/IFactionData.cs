using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    public interface IFactionData<T> where T : IUnit
    {
        List<string> GetUnitTypeIdentifiers();
        int GetUnitCost(string typeIdentifier);
        T GetUnit(string typeIdentifier);
    }
}
