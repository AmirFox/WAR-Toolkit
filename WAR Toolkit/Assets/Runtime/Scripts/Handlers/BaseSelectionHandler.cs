using WarToolkit.Core.Enums;
using System;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;

public abstract class BaseSelectionHandler
{
    protected IEventManager _eventManager;
    protected int _playerIndex;
    
    protected abstract Phase phase { get; }

    public BaseSelectionHandler(int playerIndex, IEventManager eventManager)
    {
        _playerIndex = playerIndex;
        _eventManager = eventManager;
        eventManager.StartListening(Constants.EventNames.GAME_STATE_CHANGED, OnStateChanged);
    }

    protected void OnStateChanged(IArguements args)
    {
        if(args is GameStateChangeArgs stateChangeArgs)
        {
            if(IsPhaseEntered(stateChangeArgs))
            {
                AddListeners();
            }
            else
            {
                RemoveListeners();
            }
        }
    }

    protected abstract void AddListeners();

    protected abstract void RemoveListeners();

    protected bool IsPhaseEntered(GameStateChangeArgs args)
    {
        return args.NewPlayerIndex == _playerIndex && args.NewPhase == phase;
    }
}
