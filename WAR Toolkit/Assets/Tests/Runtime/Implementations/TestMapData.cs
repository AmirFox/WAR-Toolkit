using WarToolkit.ObjectData;
using UnityEngine;

namespace WarToolkit.PlayTests
{
    public class TestMapData : IMapData<TestTile>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Seed { get; set; }
        #region PRIVATE MEMBERS
        private int _maxTileHeight;
        #endregion

        #region CTORS
        public TestMapData(int width, int height, int maxTileHeight = 1, string seed = "")
        {
            this.Seed = seed;
            this.Width = width;
            this.Height = height;
            _maxTileHeight = maxTileHeight;
        }
        #endregion

        #region PUBLIC METHODS
        public TestTile GetTileData(int x, int y)
        {
            if (_maxTileHeight == 0) throw new System.ArgumentOutOfRangeException(nameof(_maxTileHeight));

            if (_maxTileHeight == 1) return new TestTile(x, y, _maxTileHeight, 0.0, true);

            float perlinValue = GenerateNoise(x, y);
            var clamped = Mathf.Clamp(perlinValue, 0f, 1f);
            var height = Mathf.FloorToInt(clamped * _maxTileHeight);
            if (height >= _maxTileHeight)
                height = _maxTileHeight;

            return new TestTile(x, y, height, 0.0, height == 0);
        }
        #endregion

        #region PRIVATE METHODS
        private float GenerateNoise(float x, float y)
        {
            float hash = 0f;

            if (!string.IsNullOrWhiteSpace(Seed))
            {
                hash = Seed.GetHashCode();
            }

            return Mathf.PerlinNoise(x + hash, y + hash);
        }
        #endregion
    }
}
