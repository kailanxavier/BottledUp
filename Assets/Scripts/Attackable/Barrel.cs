using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : BaseAttackable
{
    public GameObject destroyThis;
    protected override void Attack()
    {
        Debug.Log("Tried to attack");
        Destroy(destroyThis);
    }
}
