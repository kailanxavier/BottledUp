using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackImpulse : MonoBehaviour
{
    private InputManager _inputManager;
    public float explosionForce = 500f;
    public float explosionRadius = 2f;

    private void Awake()
    {
        _inputManager = GetComponentInParent<InputManager>();

        _inputManager.AttackPerformed += GoBoom;
    }

    private void GoBoom()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
