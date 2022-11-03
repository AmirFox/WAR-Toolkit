namespace WarToolkit.Utilities
{
    /// <summary>
    /// Interface for generating noise using a random seed.
    /// </summary>
    public interface INoiseGenerator
    {
        public string Seed { get; }
        float GenerateNoise(float x, float y);
    }
}