using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    InputManager inputManager;
    public BaseAttackable attackable;

    public CustomCollision attackRangeCollider;

    // Attack
    public bool canAttack = false;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Awake()
    {
        attackRangeCollider.EnterTriggerZone += OnAttackTriggerEntered;
        attackRangeCollider.ExitTriggerZone += OnAttackTriggerExited;
    }

    public void Update()
    {
        HandleAttack();
    }

    public void HandleAttack()
    {
        if (inputManager.CheckForAttack() && canAttack)
        {
            attackable.BaseAttack();
        }
    }

    private void OnAttackTriggerEntered(Collider collider)
    {
        Debug.Log("In range");
        attackable = collider.GetComponent<BaseAttackable>();
        if (attackable != null)
        {
            canAttack = true;
        }
    }
    private void OnAttackTriggerExited(Collider collider)
    {
        Debug.Log("Out of range");
        attackable = null;
        canAttack = false;
    }
}
