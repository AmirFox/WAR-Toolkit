using System.Collections.Generic;
using UnityEngine;
using WarToolkit.Core.EventArgs;

namespace WarToolkit.ObjectData
{
    /// <summary>
    /// A basic implementation of tile properties on a MonoBehaviour.
    /// </summary>
    public class Tile : MonoBehaviour, ITile
    {
        private IEventManager _eventManager;

        #region PUBLIC FIELDS
        #region UI FIELDS
        [field: SerializeField] public double BaseMoveValue { get; private set; } = 1;
        [field: SerializeField] public double BaseDefenceModifier { get; private set; } = 0;
        [field: SerializeField] public bool IsAccesible { get; private set; } = true;
        #endregion

        public Vector2 Coordinates { get; private set; }
        public Vector2[] Neighbors { get; private set; } = new Vector2[4];

        public IUnit Occupier { get; set; }
        #endregion

        #region PUBLIC METHODS
        public void Initialize(int x, int y)
        {
            this.Initialize(new Vector2(x, y));
        }

        public void Initialize(Vector2 coord)
        {
            this.Coordinates = coord;
        }

        public void SetHighlight(bool highlight)
        {
            //Enable or disable highlight component
        }
        #endregion

        #region UNITY METHODS
        private void Awake() 
        {
            _eventManager = IEventManager.Instance;
        }

        private void OnMouseDown() 
        {
            IArguements eventArgs = new SelectionEventArgs<Tile>(this);
            _eventManager.TriggerEvent(Constants.EventNames.TILE_SELECTED, eventArgs);
        }
        #endregion
    }
}