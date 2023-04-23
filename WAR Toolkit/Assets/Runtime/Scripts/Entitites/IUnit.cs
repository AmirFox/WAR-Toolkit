namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for unit properties.
    /// </summary>
    public interface IUnit : IDeployable, IMovable, ICombatant
    {
        string TypeIdentifier { get; }
    }
}