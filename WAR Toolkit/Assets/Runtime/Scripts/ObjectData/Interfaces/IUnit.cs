namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for unit properties.
    /// </summary>
    public interface IUnit
    {
        string typeIdentifier { get; }

        int spawnCost { get; }

        /// <summary>
        /// Value used to evaluate combat as an agressor during the current team's combat phase.
        /// </summary>
        double BaseAttackValue { get; }
        
        /// <summary>
        /// Value used to evaluate combat as a defender during the opposing team's combat phase.
        /// </summary>
        double BaseDefenceValue { get; }

        /// <summary>
        /// How far the unit can move
        /// </summary>
        double BaseMovementValue { get; }

        /// <summary>
        /// Health status of the current unit.
        /// </summary>
        HealthStatus Status{ get; }

        /// <summary>
        /// Reset the current state of this unit.
        /// </summary>
        void ResetState();

        /// <summary>
        /// Attempt to move the unit to the given position.
        /// </summary>
        /// <param name="path">path to target</param>
        void MoveToPosition(ITile target);

        /// <summary>
        /// Attempt combat with the given unit
        /// </summary>
        /// <param name="unit">the unit being targetted</param>
        void AttackUnit(IUnit unit);
    }
    
    public enum HealthStatus { Normal, Damaged, Destroyed }
}