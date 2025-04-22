using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrip : MonoBehaviour
{
    public Collider playerCollider;

    void OnTriggerEnter(Collider other)
    {     
        Debug.Log("Player inserted");      
        if (other.bounds.Contains(playerCollider.bounds.min) 
             && other.bounds.Contains(playerCollider.bounds.max))
        {
            Debug.Log("THE PLAYER IS IN THE WATERRRRRR");
        }
    }
}
