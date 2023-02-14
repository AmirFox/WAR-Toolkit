using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;

namespace WarToolkit.ObjectData
{
    public class Unit : MonoBehaviour, IUnit
    { 
        private IEventManager _eventManager;
        private Movable _movableComponent;
        private Combatant _combatComponent;

        #region FIELDS
        public int PlayerIndex { get; set; }
        public string TypeIdentifier {get => this.gameObject.name; } 
        #endregion

        #region METHODS
        public void TurnReset()
        {
            _movableComponent?.TurnReset();
            _combatComponent?.TurnReset();
        }
        #endregion

        private void Awake() 
        {
            _eventManager = IEventManager.Instance;         
            _movableComponent = GetComponent<Movable>();
            _combatComponent = GetComponent<Combatant>();
        }

        private void OnMouseDown() 
        {
            switch(TurnManager.Instance.CurrentPhase)
            {
                case Phase.MOVEMENT:
                    SelectionEventArgs<Movable> movableArgs = new SelectionEventArgs<Movable>(_movableComponent);
                    _eventManager.TriggerEvent(Constants.EventNames.MOVABLE_SELECTED, movableArgs);
                    break;
                case Phase.COMBAT:
                    SelectionEventArgs<Combatant> combatArgs = new SelectionEventArgs<Combatant>(_combatComponent);
                    _eventManager.TriggerEvent(Constants.EventNames.COMBATANT_SELECTED, combatArgs);
                    break;
            }
        }
    }
}