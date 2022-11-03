using System.Collections.Generic;
using OpsEngine.Tilekit.Utilities;
using UnityEngine;

namespace OpsEngine.Tilekit.ObjectData
{
    [CreateAssetMenu(fileName = "Map Data", menuName = "Custom Assets/Map Data")]
    /// <summary>
    /// Scriptable object representing static map data.
    /// </summary>
    public class MapData : ScriptableObject, IMapData<Tile>, INoiseGenerator
    {
        #region UI FIELDS
        [Header("Map Configuration")]
        [Tooltip("Types of tile sorted by height.")]
        [field: SerializeField]
        private List<Tile> _tileTypes;

        [Tooltip("Map generation seed for randomization.")]
        [field: SerializeField]
        public string Seed { get; private set; }

        [Tooltip("Width of map.")]
        [field: SerializeField]
        public int Width { get; private set; } = 10;

        [Tooltip("Height of map.")]
        [field: SerializeField]
        public int Height { get; private set; } = 10;

        [Header("Noise Generation")]
        [field: SerializeField] 
        private float _frequency = 1f;
        
        [field: SerializeField]
        [Range(1,3)] 
        private int _dimensions = 3;
        
        [field: SerializeField]
        [Range(1,8)] 
        private int _ocvtaves = 1;
        
        [field: SerializeField]
        [Range(1f,4f)] 
        private float _lacurnity = 2f;

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
        public Tile GetTileData(int x, int y)
        {
            float perlinValue = GenerateNoise(x, y);
            var clamped = Mathf.Clamp(perlinValue, 0f, 1f);
            var index = Mathf.FloorToInt(clamped * _tileTypes.Count);

            if (index >= _tileTypes.Count)
                index = _tileTypes.Count - 1;

            Tile tilePrefab = _tileTypes[index];
            Tile tileInstance = Tile.Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
            tileInstance.name = $"({x},{y})";
            tileInstance.Initialize(x, y);

            return tileInstance;
        }

        public float GenerateNoise(float x, float y)
        {
            float hash = Seed.GetHashCode();
            Vector3 p = new Vector3(x, y, hash);
            NoiseMethod method = Noise.noiseMethods[(int)_noiseMethod][_dimensions - 1];
            float noiseSum = Noise.Sum(method, p, _frequency, _ocvtaves, _lacurnity, _persistence);
            return Mathf.Clamp(noiseSum, 0f, 1f);
        }
        #endregion
    }
}