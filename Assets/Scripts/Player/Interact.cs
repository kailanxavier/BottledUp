using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    
    private InputManager _inputManager;
    Interactable interactable;

    [SerializeField] private bool canInteract;
    [SerializeField] private GameObject interactButtonUI;

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

    private void OnTriggerEnter(Collider collider)
    {
        canInteract = true;
        interactable = collider.GetComponentInParent<Interactable>();
    }

    private void OnTriggerExit(Collider collider)
    {
        canInteract = false;
    }
}
