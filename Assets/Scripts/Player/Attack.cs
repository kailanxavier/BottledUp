using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    InputManager _inputManager;
    Player _player;

    private List<BaseAttackable> attackables = new();
    private Dictionary<BaseAttackable, GameObject> currentActive = new();

    public CustomCollision attackRangeCollider;

    public string attackableTag = "Attackable";
    public float attackForce = 3f;

    [SerializeField] private float buttonOffsetAmount = 2f;

    // Attack
    public bool canAttack = false;

    public ParticleManager particleManager;

    public GameObject attackButtonUI;
    public GameObject destroyParticlesPrefab;

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
        if (canAttack && _player.IsGrounded && attackables != null && attackables.Count > 0)
        {
            BaseAttackable target = GetCurrentTarget();
            if (target != null)
            {
                target.BaseAttack();
                GameObject particleInstance = Instantiate(destroyParticlesPrefab);
                particleInstance.transform.position = target.transform.position;

                Destroy(particleInstance, 0.6f);

                if (currentActive.ContainsKey(target))
                {
                    Destroy(currentActive[target]);
                    currentActive.Remove(target);
                }

                attackables.Remove(target);

                canAttack = attackables.Count > 0;
            }
        }
    }

    private BaseAttackable GetCurrentTarget()
    {
        if (attackables.Count == 0) return null;

        return attackables
            .OrderBy(a => Vector3.Distance(transform.position, a.transform.position))
            .First();
    }

    private void UpdateClosestTarget()
    {
        foreach (var ui in currentActive.Values)
        {
            Destroy(ui);
        }
        currentActive.Clear();

        if (attackables.Count > 0)
        { 
            BaseAttackable closest = GetCurrentTarget();
            if (closest != null)
            {
                // create instance of attack button UI from prefab
                GameObject buttonInstance = Instantiate(attackButtonUI);

                // set position to top of object
                buttonInstance.transform.localPosition = closest.transform.position + new Vector3(0f, buttonOffsetAmount, 0f);
                currentActive[closest] = buttonInstance;
            }
        }
    }

    private void OnAttackTriggerEntered(Collider collider)
    {
        if (collider.CompareTag(attackableTag))
        {
            BaseAttackable target = collider.GetComponent<BaseAttackable>();
            if (target != null && !attackables.Contains(target))
            {
                attackables.Add(target);
                UpdateClosestTarget();
            }
            canAttack = true;
        }
    }

    private void OnAttackTriggerExited(Collider collider)
    {
        if (collider.CompareTag(attackableTag))
        {
            BaseAttackable target = collider.GetComponent<BaseAttackable>();
            if (target != null)
            {
                attackables.Remove(target);
                UpdateClosestTarget();
            }

            canAttack = attackables.Count > 0;
        }
    }


}
