using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    InputManager inputManager;
    AnimationManager animationManager;
    Player _player;
    public BaseAttackable attackable;

    public CustomCollision attackRangeCollider;

    public string attackableTag = "Attackable";

    // Attack
    public bool canAttack = false;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        animationManager = GetComponent<AnimationManager>();
        _player = GetComponent<Player>();
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
        if (inputManager.CheckForAttack() && canAttack && _player.isGrounded)
        {
            attackable.BaseAttack();
            animationManager.HandleAttackAnimation();
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
