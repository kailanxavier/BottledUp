using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Interact : MonoBehaviour
{
    private InputManager _inputManager;
    public BaseInteractable interactable;

    public CustomCollision interactRangeCollider;

    public string interactableTag = "Interactable";

    // Interact
    public bool canInteract = false;
    public GameObject interactButtonUI;

    private bool _playerInteracted = false;

    public bool Interacted { get { return _playerInteracted; } } 

    [SerializeField] private float buttonOffsetAmount = 2f;

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
            _playerInteracted = true;
            interactable.BaseInteract();
        }
        else
        {
            _playerInteracted = false;
        }
    }

    private void MoveInteractButton(Collider collider)
    {
        if (canInteract)
        {
            interactButtonUI.transform.position = collider.transform.position + new Vector3(0f, buttonOffsetAmount, 0f);
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
                MoveInteractButton(collider);
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
