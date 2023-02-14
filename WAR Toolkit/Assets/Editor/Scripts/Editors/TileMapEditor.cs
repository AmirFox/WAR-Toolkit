using System.Collections.Generic;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace WarToolkit.Editors
{
    /// <summary>
    /// Custom inspector for generating and querying tile maps.
    /// </summary>
    [CustomEditor(typeof(MapManager))]
    public class TileMapEditor : Editor
    {
        #region PRIVATE FIELDS
        private int lastHighightPoints;
        private Vector2 lastHighlightOrigin;
        private Vector2 lastPathFindingOrigin;
        private Vector2 lastPathFindingTarget;
        private bool showMapControls = true;
        private bool showHighlighterControls = false;
        private bool showPathFindingControls = false;
        private IEnumerable<Tile> _result;
        #endregion

        #region PRIVATE METHODS
        private void DrawMapInspector(MapManager target)
        {
            if (GUILayout.Button("Generate Map"))
            {
                target.CreateMap();
            }
            if (GUILayout.Button("Clear Map"))
            {
                target.ClearMap();
            }
        }

        private void ColorSelection(Color color)
        {
            foreach (Tile tile in _result)
            {
                tile.GetComponent<IHighlight>()?.Show(color);
            }
        }

        private void ClearColor(MapManager target)
        {
            foreach (Tile tile in target.GetAll())
            {
                tile.GetComponent<IHighlight>()?.Clear();
            }
        }

        private void DrawHighlightInspector(MapManager target)
        {
            lastHighlightOrigin = EditorGUILayout.Vector2Field("Origin", lastHighlightOrigin);
            lastHighightPoints = EditorGUILayout.IntField("Points", lastHighightPoints);
            if (GUILayout.Button("HighlightTiles"))
            {
                _result = target.FindHighlight(lastHighlightOrigin, lastHighightPoints);
                ColorSelection(Color.blue);
            }
        }

        private void DrawPathFindingInspector(MapManager target)
        {
            lastPathFindingOrigin = EditorGUILayout.Vector2Field("Origin", lastPathFindingOrigin);
            lastPathFindingTarget = EditorGUILayout.Vector2Field("Target", lastPathFindingTarget);
            if (GUILayout.Button("HighlightTiles"))
            {
                _result = target.FindPath(lastPathFindingOrigin, lastPathFindingTarget);
                ColorSelection(Color.cyan);
            }
        }
        #endregion
        
        #region PUBLIC METHODS
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!Application.isPlaying) return;

            showMapControls = EditorGUILayout.Foldout(showMapControls, "Map Controls");
            if (showMapControls)
            {
                DrawMapInspector(target as MapManager);
            }

            showHighlighterControls = EditorGUILayout.Foldout(showHighlighterControls, "Highighter Controls");
            if (showHighlighterControls)
            {
                DrawHighlightInspector(target as MapManager);
            }


            showPathFindingControls = EditorGUILayout.Foldout(showPathFindingControls, "Pathfinding");
            if (showPathFindingControls)
            {
                DrawPathFindingInspector(target as MapManager);
            }

            if (GUILayout.Button("Clear Highlights"))
            {
                ClearColor(target as MapManager);

                _result = new List<Tile>();
            }
        }
        #endregion
    }
}
#endif
