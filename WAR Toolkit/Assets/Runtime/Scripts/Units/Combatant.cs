using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour, ICombatant
{
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
}
