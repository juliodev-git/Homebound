using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * This class pretty much attaches to any game object as a child, setting the parent transform as the target for the Interaction Camera
 * Checks player input, controls enabled status of Interaction Virtual Camera and Player animator
 */

[RequireComponent(typeof(BoxCollider))]
public class Interaction : MonoBehaviour
{
    private BoxCollider _bc;
    private InteractionCamera _ic;
    private Transform _parent;
    private TextMeshProUGUI _interactText;
    private UIController _ui;

    private void Awake()
    {
        _ic = GameObject.Find("InteractionCameraComponents").GetComponent<InteractionCamera>();
        _interactText = GameObject.Find("Interact").GetComponent<TextMeshProUGUI>();
        _ui = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    private void Start()
    {
        _bc = GetComponent<BoxCollider>();
        _parent = GetComponentInParent<Transform>();
        
        //initialize/standardize interaction triggers
        _bc.isTrigger = true;

        //make the collider a trigger, make the size consistent
        _bc.size = new Vector3(3.0f, 1.0f, 3.0f);

        _interactText.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //when player is in the trigger, check for player Input
        if (other.CompareTag("Player")) {
            if (_ic) {
                _interactText.enabled = true;
                if (Human.GetInteract()) {
                    //player pressed A while in trigger...

                    //disable interact text
                    _interactText.enabled = false;

                    //disable trigger
                    _bc.enabled = false;

                    //enable camera
                    _ic.EnableCamera();

                    //enable UI
                    EnableInteractionUI();

                    //make player go into interaction animation
                    Human.SetInteract(true, _parent);
                    
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        _interactText.enabled = false;
    }

    void EnableInteractionUI() {
        StartCoroutine("EnableInteractUI");
    }

    public void ExitInteraction() {
        //small delay to ensure camera has blended back to original position before properly exiting
        StartCoroutine("ExitDelay");
    }

    IEnumerator EnableInteractUI()
    {
        float delay = _ic.GetBlendTime();

        for (float i = 0; i < delay; i += Time.fixedDeltaTime)
        {
            yield return null;
        }

        _ui.ToggleButton(true);
        _ui.ToggleMenu(true);
    }

    IEnumerator ExitDelay()
    {
        _ui.ToggleButton(false);
        _ui.ToggleMenu(false);
        float delay = _ic.GetBlendTime();

        for (float i = 0; i < delay; i += Time.fixedDeltaTime)
        {
            yield return null;
        }

        _ic.DisableCamera();
        Human.SetInteract(false, null);
        _bc.enabled = true;
    }
}
