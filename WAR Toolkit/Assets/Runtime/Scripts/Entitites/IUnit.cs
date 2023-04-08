namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Interface for unit properties.
    /// </summary>
    public interface IUnit
    {
        int PlayerIndex { set; get; }

        string TypeIdentifier { get; }
    }
}