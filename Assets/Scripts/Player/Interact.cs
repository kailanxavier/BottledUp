using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private InputManager _inputManager;
    public BaseInteractable interactable;

    public ParticleManager particleManager;
    public Transform particleTransform;

    public CustomCollision interactRangeCollider;

    public string interactableTag = "Interactable";

    // Interact
    public bool canInteract = false;

    [SerializeField] private float buttonOffsetAmount = 2f;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.InteractPerformed += HandleInteraction;

        // custom collider assignment to own methods
        interactRangeCollider.EnterTriggerZone += OnInteractTriggerEntered;
        interactRangeCollider.ExitTriggerZone += OnInteractTriggerExited;
    }

    private void HandleInteraction()
    {
        if (canInteract && interactable != null)
        {
            interactable.BaseInteract();
            interactable = null;
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
            }
        }
    }

    private void OnInteractTriggerExited(Collider collider)
    {
        if (collider.CompareTag(interactableTag))
        {
            canInteract = false;
            interactable = null;
        }
    }
}
