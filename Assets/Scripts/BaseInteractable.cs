using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    { 
        // Parent method to be inherited.
    }
}