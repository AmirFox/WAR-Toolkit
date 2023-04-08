using UnityEngine;
using WarToolkit.Managers;
using Zenject;

namespace WarToolkit.ObjectData
{
    public class Unit : MonoBehaviour, IUnit
    { 
        [Inject] private TurnManager _turnManager;
        [Inject] private IEventManager _eventManager;

        #region FIELDS
        public int PlayerIndex { get; set; }
        public string TypeIdentifier {get => this.gameObject.name; } 
        #endregion

        #region METHODS
        #endregion

        private void Awake() 
        {
        }
    }
}