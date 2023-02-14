using WarToolkit.ObjectData;

public interface ICombatant
{
    bool HasAttacked { get; }
    
        /// <summary>
        /// Value used to evaluate combat as an agressor during the current team's combat phase.
        /// </summary>
        double BaseAttackValue { get; }
        
        /// <summary>
        /// Value used to evaluate combat as a defender during the opposing team's combat phase.
        /// </summary>
        double BaseDefenceValue { get; }

        /// <summary>
        /// Health status of the current unit.
        /// </summary>
        HealthStatus Status{ get; }
        
        /// <summary>
        /// Attack the target combatant.
        /// </summary>
        /// <param name="target"></param>
        void AttackCombatant(ICombatant target);

        void TurnReset();
}

public enum HealthStatus { Normal, Damaged, Destroyed }
