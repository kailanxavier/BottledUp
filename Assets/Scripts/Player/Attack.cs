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

    public GameObject attackButtonUI;
    [SerializeField] private float buttonOffsetAmount = 2f;

    // Attack
    public bool canAttack = false;

    public ParticleManager particleManager;
    public Transform particlesTransform;

    private void Awake()
    {
        // init
        _inputManager = GetComponent<InputManager>();
        _player = GetComponent<Player>();

        _inputManager.AttackPerformed += HandleAttack;

        attackRangeCollider.EnterTriggerZone += OnAttackTriggerEntered;
        attackRangeCollider.ExitTriggerZone += OnAttackTriggerExited;

        // turn ui off
        attackButtonUI.SetActive(false);
    }

    private void MoveInteractButtonAndParticles(Collider collider)
    {
        attackButtonUI.transform.position = collider.transform.position + new Vector3(0f, buttonOffsetAmount, 0f);
        particlesTransform.transform.position = collider.transform.position;
    } 

    private void HandleAttack()
    {
        if (canAttack && _player.IsGrounded && attackable != null)
        {
            attackable.BaseAttack();
            particleManager.HandleBreakingParticles();
            attackButtonUI.SetActive(false);
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
                MoveInteractButtonAndParticles(collider);
                canAttack = true;
                attackButtonUI.SetActive(canAttack);
            }
        }
    }

    private void OnAttackTriggerExited(Collider collider)
    {
        if (collider.CompareTag(attackableTag))
        {
            attackable = null;
            canAttack = false;
            attackButtonUI.SetActive(canAttack);
        }
    }
}
