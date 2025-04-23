using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private InputManager _inputManager;
    public float explosionForce = 500f;
    public float explosionRadius = 2f;

    [SerializeField] private LayerMask _physicsObjects;

    private void Awake()
    {
        _inputManager = GetComponentInParent<InputManager>();

        _inputManager.AttackPerformed += GoBoom;
    }

    public void GoBoom()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,
                                                     explosionRadius,
                                                     _physicsObjects);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
