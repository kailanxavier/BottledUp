using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollision : MonoBehaviour
{
    // This is a class to handle the two different 
    // ranges the game has right now. Interact and attack.
    // And the improved ground check now.

    public event System.Action<Collider> EnterTriggerZone;
    public event System.Action<Collider> ExitTriggerZone;

    private void OnTriggerEnter(Collider collider)
    {
        EnterTriggerZone?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        ExitTriggerZone?.Invoke(collider);
    }
}
