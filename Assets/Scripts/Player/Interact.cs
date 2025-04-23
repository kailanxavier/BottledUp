using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    private InputManager _inputManager;
    public BaseInteractable interactable;

    public CustomCollision interactRangeCollider;

    public string interactableTag = "Interactable";

    // Interact
    public bool canInteract = false;
    public GameObject interactButtonUI;

    private void Awake()
    {
        // interact button invisible when game starts
        interactButtonUI.SetActive(false);

        _inputManager = GetComponent<InputManager>();
        _inputManager.InteractPerformed += HandleInteraction;

        interactRangeCollider.EnterTriggerZone += OnInteractTriggerEntered;
        interactRangeCollider.ExitTriggerZone += OnInteractTriggerExited;
    }

    private void HandleInteraction()
    {
        if (canInteract)
        {
            interactable.BaseInteract();
        }
    }

    // interact range trigger
    private void OnInteractTriggerEntered(Collider collider)
    {
        // only update when the tag matches
        if (collider.CompareTag(interactableTag))
        {
            interactable = collider.gameObject.GetComponent<BaseInteractable>();

            if (interactable != null)
            {
                canInteract = true;
                interactButtonUI.SetActive(true);
            }

        }
    }

    private void OnInteractTriggerExited(Collider collider)
    {
        if (collider.CompareTag(interactableTag))
        {
            canInteract = false;
            interactable = null;
            interactButtonUI.SetActive(false);
        }
    }
}
