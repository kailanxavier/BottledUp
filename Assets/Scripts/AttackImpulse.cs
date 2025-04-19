using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackImpulse : MonoBehaviour
{
    private InputManager inputManager;
    public float explosionForce = 500f;
    public float explosionRadius = 2f;

    private void Awake()
    {
        inputManager = GetComponentInParent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.CheckForAttack())
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider hit in colliders)
            { 
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null && hit != null)
                { 
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
    }
}
