using UnityEngine;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// Test highlighter implementation for coloring sprite renderer on an object.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class TestHighlight : MonoBehaviour, IHighlight
    {
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
        }

        public void Show(Color color)
        {
            spriteRenderer.color = color;
        }

        public void Clear()
        {
            spriteRenderer.color = originalColor;
        }
    }
}