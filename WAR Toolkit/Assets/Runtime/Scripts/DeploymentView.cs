using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WarToolkit.Core.EventArgs;
using WarToolkit.Managers;
using WarToolkit.ObjectData;
using static Constants;

public class DeploymentView : MonoBehaviour
{
    [Zenject.Inject] private IEventManager _eventManager;
    [Zenject.Inject] private ITurnManager _turnManager; 
    [SerializeField] private GameObject deploymentButtonPrefab;
    [SerializeField] private GameObject buttonPanel;

    void Awake()
    {
        _eventManager.StartListening(EventNames.GAME_STATE_CHANGED, OnPhaseChanged);
    }

    private void OnPhaseChanged(IArguements args)
    {
        if(args is GameStateChangeArgs asGameStateArgs)
        {
            if(asGameStateArgs.NewPhase == WarToolkit.Core.Enums.Phase.DEPLOYMENT)
            {
                LoadUnitButtons(asGameStateArgs.NewPlayerIndex);
            }
            else
            {
                ClearButtons();
            }
        }
    }

    private void ClearButtons() 
    {
        foreach(Button button in buttonPanel.GetComponentsInChildren<Button>())
        {
            GameObject.Destroy(button);
        }
    }

    private void LoadUnitButtons(int playerIndex)
    {
        if(!_turnManager.TryGetPlayer(playerIndex, out IPlayer player)) return;

        for(int i = 0; i < player.factionData.RosterSize; i++)
        {
            CreateButton(playerIndex, i);
        }
    }

    private void CreateButton(int playerIndex, int deployableIndex)
    {
        GameObject button = GameObject.Instantiate(deploymentButtonPrefab);
        button.SetActive(true);

        DeployableSelectedEventArgs args = new DeployableSelectedEventArgs(playerIndex, deployableIndex);
        button.GetComponent<Button>().onClick.AddListener(()=>_eventManager.TriggerEvent(Constants.EventNames.DEPLOYABLE_SELECTED, args));
        button.transform.SetParent(buttonPanel.transform);
    }
}
