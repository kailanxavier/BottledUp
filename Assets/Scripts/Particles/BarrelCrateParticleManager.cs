using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCrateParticleManager : BaseAttackable
{

    public ParticleSystem WoodSystem;
    public ParticleSystem SmokeSystem;


    protected override void Attack()
    {
        WoodSystem.Play();
        SmokeSystem.Play();
    }

}
