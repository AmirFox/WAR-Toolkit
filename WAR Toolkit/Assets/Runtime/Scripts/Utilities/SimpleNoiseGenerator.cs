using UnityEngine;

namespace OpsEngine.Tilekit.Utilities
{
    /// <summary>
    /// Generates noise using the Unity perlin noise math function.
    /// </summary>
    public class SimpleNoiseGenerator : INoiseGenerator
    {
        public string Seed { get; private set; }

        public SimpleNoiseGenerator(string seed = "")
        {
            this.Seed = seed;
        }

        public float GenerateNoise(float x, float y)
        {
            float hash = 0f;

            if (!string.IsNullOrWhiteSpace(Seed))
            {
                hash = Seed.GetHashCode();
            }

            return Mathf.PerlinNoise(x + hash, y + hash);
        }
    }
}