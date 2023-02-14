using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WarToolkit.Utilities;

public class MapGenerator : MonoBehaviour
{
    #region FIELDS
    [field:SerializeField]
    public Tilemap _tileMap {get; private set;}
    [field:SerializeField]
    public Tilemap _overlayTiles {get; private set; }

    [field:SerializeField]
    public ScriptableTile[] _terrains {get; private set;}

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


    private void Start() {
        Generate();
    }

    void Generate()
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                ScriptableTile tile = GetTileData(x,y);
                _tileMap.SetTile(new Vector3Int(x,y), tile as TileBase);
                if(tile.overlayTile != null)
                {
                    _overlayTiles.SetTile(new Vector3Int(x,y), tile.overlayTile);
                }
            }
        }
    }


            #region PUBLIC METHODS
        /// <summary>
        /// Retrieve the tile at the given position.
        /// </summary>
        /// <param name="x">X co-ordinate of tile</param>
        /// <param name="y">Y co-ordinate of tile</param>
        /// <returns>Returns the found tile or null.</returns>
        public ScriptableTile GetTileData(int x, int y)
        {
            float perlinValue = GenerateNoise(x, y);
            var clamped = Mathf.Clamp(perlinValue, 0f, 1f);
            var index = Mathf.FloorToInt(clamped * _terrains.Length);

            if (index >= _terrains.Length)
                index = _terrains.Length - 1;

            return _terrains[index];
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
