using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Barrel : BaseInteractable
{
    public GameObject ObjectToDestroy;

    protected override void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        Destroy(ObjectToDestroy);
    }
}
