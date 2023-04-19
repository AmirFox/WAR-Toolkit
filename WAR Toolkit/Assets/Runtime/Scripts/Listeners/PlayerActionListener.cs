using WarToolkit.Core.Enums;
using System;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;
using Zenject;
using UnityEngine;
using static Constants;

public class DebugSelectionListener  : MonoBehaviour
{
    [Inject]
    private IEventManager _eventManager;

    [Inject]
    private IMapController _mapController;

    private void OnEnable() 
    {
        _eventManager.StartListening(EventNames.TILE_SELECTED, OnTileSelected);  
    }
    
    private void OnDisable() 
    {
        _eventManager.StopListening(EventNames.TILE_SELECTED, OnTileSelected);        
    }

    private void OnTileSelected(IArguements args)
    {
        if(args is SelectionEventArgs<ITile> asSelectionArgs)
        {
            Debug.Log($"Tile at : { asSelectionArgs.Selection.DisplayName } selected.");
        }
    }
}

public abstract class PlayerActionListener
{
    protected IEventManager _eventManager;

    protected int _playerIndex;
    
    protected abstract Phase phase { get; }

    public PlayerActionListener(int playerIndex, IEventManager eventManager)
    {
        _eventManager = eventManager;
        _playerIndex = playerIndex;
        eventManager.StartListening(Constants.EventNames.GAME_STATE_CHANGED, OnStateChanged);
    }

    protected void OnStateChanged(IArguements args)
    {
        if(args is GameStateChangeArgs stateChangeArgs)
        {
            if(IsPhaseEntered(stateChangeArgs))
            {
                
                Debug.Log($"GAME STATE ENTERED: {stateChangeArgs.NewPhase}");
                StartListening();
            }
            else
            {
                StopListening();
            }
        }
    }
    
    protected abstract void StartListening();

    protected abstract void StopListening();

    protected bool IsPhaseEntered(GameStateChangeArgs args)
    {
        return args.NewPlayerIndex == _playerIndex && args.NewPhase == phase;
    }
}
