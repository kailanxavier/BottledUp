using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : BaseAttackable
{
    protected override void Attack()
    {
        Debug.Log("Tried to attack");
        Destroy(this.gameObject);
    }
}
