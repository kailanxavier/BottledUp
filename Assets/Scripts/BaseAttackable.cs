using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackable : MonoBehaviour
{
    public void BaseAttack()
    {
        Attack();
        Destroy(gameObject);
    }

    protected virtual void Attack()
    {
        // Parent method to be inherited.
    }
}
