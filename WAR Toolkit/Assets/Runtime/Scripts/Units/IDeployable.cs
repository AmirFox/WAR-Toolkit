using UnityEngine;
using WarToolkit.ObjectData;

public interface IDeployable
{
    int Cost { get; }
    bool IsDeployed { get; }

    void Deploy(Vector2 position);
}