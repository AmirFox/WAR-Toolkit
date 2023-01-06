using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.ObjectData;

namespace WarToolkit.ObjectData
{
    public class Unit : MonoBehaviour, IUnit
    {
        private TilePath<ITile> _currentPath;
        private bool _hasMoved = false;
        private bool _hasAttacked = false;

        public string typeIdentifier {get => this.gameObject.name; } 
        [field: SerializeField] public int spawnCost { get; private set; }
        [field: SerializeField] public double BaseAttackValue { get; private set; }

        [field: SerializeField] public double BaseDefenceValue { get; private set; }

        [field: SerializeField] public double BaseMovementValue { get; private set; }

        public HealthStatus Status { get; private set; } = HealthStatus.Normal;

        public void ResetState()
        {
            _hasMoved = false;
            _hasAttacked = false;
        }

        public void MoveToPosition(ITile tile)
        {
            _hasMoved = true;
        }

        public void AttackUnit(IUnit unit)
        {
            _hasAttacked = true;
        }

        private void Update() 
        {
        }
    }
}