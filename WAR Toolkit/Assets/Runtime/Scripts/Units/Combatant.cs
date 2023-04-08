using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using Zenject;

[RequireComponent(typeof(IUnit))]
public class Combatant : MonoBehaviour, ICombatant
{
    [Inject] private TurnManager _turnManager;
    [Inject] private IEventManager _eventManager;

    public bool HasAttacked { get; private set; }

    [field: SerializeField] public double BaseAttackValue { get; private set; }

    [field: SerializeField] public double BaseDefenceValue { get; private set; }

    public HealthStatus Status { get; private set; } = HealthStatus.Normal;

    public void AttackCombatant(ICombatant target)
    {

    }

    public void TurnReset()
    {
        HasAttacked = false;
    }

    private void OnMouseDown()
    {
        if(_turnManager.CurrentPhase == WarToolkit.Core.Enums.Phase.COMBAT)
        {
            SelectionEventArgs<Combatant> combatArgs = new SelectionEventArgs<Combatant>(this);
            _eventManager.TriggerEvent(Constants.EventNames.COMBATANT_SELECTED, combatArgs);
        }
    }
}
