using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Chest : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}
