using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScrip : MonoBehaviour
{
    public Collider waterCollider;

    public CustomCollision waterCustomCollider;

    private void Awake()
    {
        waterCustomCollider.StayTriggerZone += StayInWater;
    }

    void StayInWater(Collider collider)
    {
        Debug.Log(waterCollider.name + " TOUCHED " + collider.name);

        if (collider.bounds.Contains(waterCollider.bounds.min) && collider.bounds.Contains(waterCollider.bounds.max))
        {
            Debug.Log("HELLO I am undah da watah pwease help me");
        }
    }
}
