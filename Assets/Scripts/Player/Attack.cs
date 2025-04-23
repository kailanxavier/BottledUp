using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    InputManager _inputManager;
    Player _player;
    public BaseAttackable attackable;

    public CustomCollision attackRangeCollider;

    public string attackableTag = "Attackable";
    public float attackForce = 3f;

    // Attack
    public bool canAttack = false;

    private void Awake()
    {
        // init
        _inputManager = GetComponent<InputManager>();
        _player = GetComponent<Player>();

        _inputManager.AttackPerformed += HandleAttack;

        attackRangeCollider.EnterTriggerZone += OnAttackTriggerEntered;
        attackRangeCollider.ExitTriggerZone += OnAttackTriggerExited;
    }

    private void HandleAttack()
    {
        if (canAttack && _player.IsGrounded)
        {
            attackable.BaseAttack();
            attackable = null;
            canAttack = false;
        }
    }

    private void OnAttackTriggerEntered(Collider collider)
    {
        if (collider.CompareTag(attackableTag))
        {
            attackable = collider.GetComponent<BaseAttackable>();
            if (attackable != null)
            {
                canAttack = true;
            }
        }
    }

    private void OnAttackTriggerExited(Collider collider)
    {
        if (collider.CompareTag(attackableTag))
        {
            attackable = null;
            canAttack = false;
        }
    }
}
