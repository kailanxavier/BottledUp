using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField] private Transform interactionDirection;
    [SerializeField] private float interactDistance = 7f;
    [SerializeField] private LayerMask interactableMask;
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
        interactButtonUI.SetActive(false);
        float interactOffset = 0.5f;
        Ray ray = new Ray(interactionDirection.position - Vector3.up * interactOffset, interactionDirection.forward);
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, interactableMask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                interactButtonUI.SetActive(true);
                if (_inputManager.CheckForInteraction())
                { 
                    interactable.BaseInteract();
                }
            }
        }
    }
}
