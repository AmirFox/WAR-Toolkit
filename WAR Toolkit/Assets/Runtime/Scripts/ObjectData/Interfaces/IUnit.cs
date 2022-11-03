namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for unit properties.
    /// </summary>
    public interface IUnit
    {
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
    }
    
    public enum HealthStatus { Normal, Damaged, Destroyed }
}