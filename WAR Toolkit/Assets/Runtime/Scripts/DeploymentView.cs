using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WarToolkit.Core.EventArgs;
using WarToolkit.ObjectData;

public class DeploymentView : MonoBehaviour
{
    private GameObject deploymentButtonPrefab;
    private GameObject buttonPanel;
    private IEventManager eventManager;

    private void ClearButtons() { }

    private void LoadUnitButtons(IEnumerable<IDeployable> deployables)
    {
        foreach(IDeployable deployable in deployables)
        {
            GameObject button = GameObject.Instantiate(deploymentButtonPrefab);

            //load a new unit button with the deployable data
            if(deployable is Deployable deployableComponent)
            {
                button.GetComponentInChildren<SpriteRenderer>().sprite = deployableComponent.GetComponent<SpriteRenderer>().sprite;
            }
            button.GetComponent<Button>().onClick.AddListener(()=>OnButtonClicked(deployable));
        }
    }

    private void OnButtonClicked(IDeployable deployable)
    {
        SelectionEventArgs<IDeployable> args = new SelectionEventArgs<IDeployable>(deployable);
        eventManager.TriggerEvent(Constants.EventNames.DEPLOYABLE_SELECTED, args);
    }
}
