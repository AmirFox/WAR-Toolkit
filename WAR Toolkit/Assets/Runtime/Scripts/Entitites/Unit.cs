using UnityEngine;
using WarToolkit.Managers;
using Zenject;

namespace WarToolkit.ObjectData
{
    public class Unit : MonoBehaviour, IUnit
    {
        #region FIELDS
        public string TypeIdentifier {get => this.gameObject.name; }

        public bool IsDeployed { get; private set; }

        public Vector2 CurrentTile {get;}
        public HealthStatus Status {get;} = HealthStatus.Normal;
        public bool HasMoved { get; private set; }
        public bool HasAttacked { get; private set; }
        
        #region SERIALIZABLE
        [field: SerializeField]
        public int Cost {get; private set;}
        [field: SerializeField]
        public int BaseMovementValue {get; private set;}
        [field: SerializeField]
        public double BaseAttackValue {get; private set;}
        [field: SerializeField]
        public double BaseDefenceValue {get; private set;}
        #endregion
        #endregion

        #region METHOD IMPLEMENTATIONS
        public void AttackCombatant(ICombatant target)
        {
            throw new System.NotImplementedException();
        }

        public void Deploy(Vector2 position)
        {
            GameObject.Instantiate(this, position, Quaternion.identity);
            
            IsDeployed = true;
        }

        public void MoveToTile(Vector2 tile)
        {
            throw new System.NotImplementedException();
        }

        public void TurnReset()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}