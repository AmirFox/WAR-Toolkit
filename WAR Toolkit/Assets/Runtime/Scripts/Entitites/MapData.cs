using System.Collections.Generic;
using WarToolkit.Utilities;
using UnityEngine;

namespace WarToolkit.ObjectData
{
    [CreateAssetMenu(fileName = "Map Data", menuName = "Custom Assets/Map Data")]
    /// <summary>
    /// Scriptable object representing static map data.
    /// </summary>
    public class MapData : ScriptableObject, INoiseGenerator
    {
        #region UI FIELDS
        [Header("Map Configuration")]
        [Tooltip("Types of tile sorted by height.")]
        [field: SerializeField]
        private DataTile[] _tileTypes;

        [field: SerializeField]
        private RuleTile[] _overlayTiles;

        [field: SerializeField]
        public RuleTile highlightTile {get; private set; }

        [Tooltip("Map generation seed for randomization.")]
        [field: SerializeField]
        public string Seed { get; private set; }

        [Tooltip("Width of map.")]
        [field: SerializeField]
        public int Width { get; private set; } = 100;

        [Tooltip("Height of map.")]
        [field: SerializeField]
        public int Height { get; private set; } = 100;

        [Header("Noise Generation")]
        [field: SerializeField] 
        private float _frequency = 0.1f;
        
        [field: SerializeField]
        [Range(1,3)] 
        private int _dimensions = 3;
        
        [field: SerializeField]
        [Range(1,8)] 
        private int _ocvtaves = 6;
        
        [field: SerializeField]
        [Range(1f,4f)] 
        private float _lacurnity = 1f;

        [field: SerializeField]
        [Range(0f,1f)] 
        private float _persistence = 0.5f;

        [field: SerializeField] 
        private NoiseMethodType _noiseMethod;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Retrieve the tile at the given position.
        /// </summary>
        /// <param name="x">X co-ordinate of tile</param>
        /// <param name="y">Y co-ordinate of tile</param>
        /// <returns>Returns the found tile or null.</returns>
        public DataTile GetTileData(int x, int y)
        {
            float perlinValue = GenerateNoise(x, y);
            var clamped = Mathf.Clamp01(perlinValue);
            var index = Mathf.FloorToInt(clamped * _tileTypes.Length);

            if (index >= _tileTypes.Length)
                index = _tileTypes.Length - 1;

            return _tileTypes[index];
        }

        public RuleTile GetOverlayTile(int x, int y)
        {
            float perlinValue = GenerateNoise(x, y);
            var clamped = Mathf.Clamp01(perlinValue);
            var index = Mathf.FloorToInt(clamped * _tileTypes.Length);

            if (index >= _overlayTiles.Length)
                return null;

            return _overlayTiles[index];
        }

        public float GenerateNoise(float x, float y)
        {
            float hash = Seed.GetHashCode();
            Vector3 p = new Vector3(x, y, hash);
            NoiseMethod method = Noise.noiseMethods[(int)_noiseMethod][_dimensions - 1];
            float noiseSum = Noise.Sum(method, p, _frequency, _ocvtaves, _lacurnity, _persistence);
            return noiseSum;
        }
        #endregion
    }
}