using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public CustomCollision interactRangeCollider;
    public string interactableTag = "Interactable";
    public float offsetAmount;

    private void Awake()
    {
        interactRangeCollider.EnterTriggerZone += OnInteractTriggerEntered;
    }

    private void OnInteractTriggerEntered(Collider collider)
    {
        if (collider.CompareTag(interactableTag))
        {
            transform.position = collider.gameObject.transform.position + new Vector3(0, offsetAmount, 0);
        }
    }

}
