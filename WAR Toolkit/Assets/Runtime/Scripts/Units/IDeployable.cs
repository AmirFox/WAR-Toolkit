using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.ObjectData;

public interface IDeployable
{
    int Cost { get; }
    bool IsDeployed { get; }

    void Deploy<T>(T tile) where T : ITile;
}