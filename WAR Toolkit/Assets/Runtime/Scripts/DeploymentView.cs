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
    [Zenject.Inject] private IEventManager eventManager;
    [Zenject.Inject] private TurnManager turnManager; 
    [SerializeField] private GameObject deploymentButtonPrefab;
    [SerializeField] private GameObject buttonPanel;

    void Awake()
    {
        eventManager.StartListening(EventNames.GAME_STATE_CHANGED, OnPhaseChanged);
    }

    private void OnPhaseChanged(IArguements args)
    {
        if(args is GameStateChangeArgs asGameStateArgs)
        {
            if(asGameStateArgs.NewPhase == WarToolkit.Core.Enums.Phase.DEPLOYMENT)
            {
                LoadUnitButtons(turnManager.CurrentPlayer.factionData.Deployables);
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

    private void LoadUnitButtons(IEnumerable<IDeployable> deployables)
    {
        foreach(IDeployable deployable in deployables)
        {
            GameObject button = GameObject.Instantiate(deploymentButtonPrefab);

            //load a new unit button with the deployable data
            if(deployable is Deployable deployableComponent)
            {
                button.SetActive(true);
                button.transform.GetChild(0).GetComponent<Image>().sprite = deployableComponent.GetComponent<SpriteRenderer>().sprite;
            }
            button.GetComponent<Button>().onClick.AddListener(()=>OnButtonClicked(deployable));
            button.transform.SetParent(buttonPanel.transform);
        }
    }

    private void OnButtonClicked(IDeployable deployable)
    {
        SelectionEventArgs<IDeployable> args = new SelectionEventArgs<IDeployable>(deployable);
        eventManager.TriggerEvent(Constants.EventNames.DEPLOYABLE_SELECTED, args);
    }
}
