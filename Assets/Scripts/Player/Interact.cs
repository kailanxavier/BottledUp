using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{

    private InputManager _inputManager;
    BaseInteractable interactable;

    public CustomCollision interactRangeCollider;

    // Interact
    public bool canInteract = false;
    public GameObject interactButtonUI;

    private void Awake()
    {
        interactRangeCollider.EnterTriggerZone += OnInteractTriggerEntered;
        interactRangeCollider.ExitTriggerZone += OnInteractTriggerExited;
    }

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        HandleInteraction();
    }

    public void HandleInteraction()
    {
        interactButtonUI.SetActive(canInteract);
        if (_inputManager.CheckForInteraction() && canInteract)
        {
            interactable.BaseInteract();
        }
    }

    // Interact range trigger
    private void OnInteractTriggerEntered(Collider collider)
    {
        interactable = collider.gameObject.GetComponent<BaseInteractable>();

        if (interactable != null)
        {
            canInteract = true;
        }
    }

    private void OnInteractTriggerExited(Collider collider)
    {
        canInteract = false;
        interactable = null;
    }
}
