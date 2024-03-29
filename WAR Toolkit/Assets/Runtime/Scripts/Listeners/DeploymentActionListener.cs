using WarToolkit.Core.Enums;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;

public class DeploymentActionListener<T_Tile, T_Deployable> : PlayerActionListener 
    where T_Tile: ITile 
    where T_Deployable : IDeployable
{
    private T_Tile[] _deploymentTiles;

    private T_Deployable _selectedDeployable;
    [Zenject.Inject] TurnManager _turnManager;

    protected override Phase phase => Phase.DEPLOYMENT;

    public DeploymentActionListener(int playerIndex, IEventManager eventManager, T_Tile[] deploymentTiles) : base(playerIndex, eventManager)
    { 
        _deploymentTiles = deploymentTiles;
    }

    protected override void StartListening()
    {
        _eventManager.StartListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
        _eventManager.StartListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
    }

    protected override void StopListening()
    {
        _eventManager.StopListening(Constants.EventNames.DEPLOYABLE_SELECTED, OnDeployableSelected);
        _eventManager.StopListening(Constants.EventNames.TILE_SELECTED, OnTileSelected);
    }

    private void OnTileSelected(IArguements args)
    {
        if(args is SelectionEventArgs<T_Tile> selectedTileArgs)
        {
            if(_selectedDeployable != null)
            {
                _turnManager.CurrentPlayer.Deploy(_selectedDeployable, selectedTileArgs.Selection);
                ClearSelection();
            }
        }
    }

    private void OnDeployableSelected(IArguements args)
    {
        if(args is SelectionEventArgs<T_Deployable> selectionArgs)
        {
            if(!selectionArgs.Selection.IsDeployed)
            {
                _selectedDeployable = selectionArgs.Selection;
                SetSelection();
            }
        }
    }

    private void SetSelection()
    {
        foreach(T_Tile tile in _deploymentTiles)
        {
            tile.SetHighlight(true);
        }
    }

    private void ClearSelection()
    {
        foreach(T_Tile tile in _deploymentTiles)
        {
            tile.SetHighlight(false);
        }
    }
}
