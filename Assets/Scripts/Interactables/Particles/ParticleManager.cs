using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public Interact interact;
    public ParticleSystem smokeParticles;
    public ParticleSystem woodSystem;

    public void HandleBreakingParticles()
    {
        smokeParticles.Play();
        woodSystem.Play();
    }
}
