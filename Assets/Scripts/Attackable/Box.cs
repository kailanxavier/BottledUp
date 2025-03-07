using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : BaseAttackable
{
    protected override void Attack()
    {
        Debug.Log("Attacked box and it dropped: NOTHING!!!");
    }
}
