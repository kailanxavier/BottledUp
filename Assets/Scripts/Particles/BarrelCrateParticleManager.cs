using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCrateParticleManager : BaseInteractable
{

    public ParticleSystem WoodSystem;
    public ParticleSystem SmokeSystem;


    protected override void Interact()
    {
        WoodSystem.Play();
        SmokeSystem.Play();
    }

}
