using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Barrel : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}
