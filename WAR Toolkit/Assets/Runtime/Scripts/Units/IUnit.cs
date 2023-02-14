namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for unit properties.
    /// </summary>
    public interface IUnit
    {
        int PlayerIndex { set; get; }

        string TypeIdentifier { get; }

        /// <summary>
        /// Reset the current state of this unit for a new turn.
        /// </summary>
        void TurnReset();
    }
}