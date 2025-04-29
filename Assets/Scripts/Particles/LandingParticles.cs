using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticles : MonoBehaviour
{
    public ParticleSystem LandingParticlesSystem;

    public void PlayLandingParticlesSystem()
    {
        LandingParticlesSystem.Play();
    }
}
