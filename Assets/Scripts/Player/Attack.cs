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
            attackable = null;
            canAttack = false;
        }
    }

    private void OnAttackTriggerEntered(Collider collider)
    {
        attackable = collider.GetComponent<BaseAttackable>();
        if (attackable != null)
        {
            canAttack = true;
        }
    }
    private void OnAttackTriggerExited(Collider collider)
    {
        attackable = null;
        canAttack = false;
    }
}
