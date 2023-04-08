using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.ObjectData;
using Zenject;
using WarToolkit.Managers;

[RequireComponent(typeof(IUnit))]
public class Deployable : MonoBehaviour, IDeployable
{
    [Inject]private ITurnManager _turnManager;
    [field: SerializeField] public int Cost { get; private set; }
    public bool IsDeployed { get; private set; }

    public void Deploy<T>(T tile) where T : ITile
    {
        /*
        puts unit onto tile
        */

    }
}
