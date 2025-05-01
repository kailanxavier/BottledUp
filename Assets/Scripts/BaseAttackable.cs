using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackable : MonoBehaviour
{
    public void BaseAttack()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        Destroy(gameObject);
        // Parent method to be inherited.
    }
}
