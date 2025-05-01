using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BaseAttackable
{
    [SerializeField] private Transform keyHolder;
    public bool hasKey;

    private BoxCollider keyCollider;
    
    void Awake()
    {
        keyCollider = GetComponent<BoxCollider>();
    }
    
    void Update()
    {
        // update key position and rotation
        if (hasKey) 
        {
            gameObject.transform.position = keyHolder.position;
            gameObject.transform.rotation = keyHolder.rotation; 
        }
    }

    protected override void Attack()
    {
        Debug.Log("you picked up the key");
        hasKey = true;
        keyCollider.enabled = false;
    }
}
