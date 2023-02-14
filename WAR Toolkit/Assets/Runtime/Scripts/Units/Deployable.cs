using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.ObjectData;

public class Deployable : MonoBehaviour, IDeployable
{
    public int Cost { get; }
    public bool IsDeployed { get; private set; }

    public void Deploy<T>(T tile) where T : ITile
    {
        throw new System.NotImplementedException();
    }
}
