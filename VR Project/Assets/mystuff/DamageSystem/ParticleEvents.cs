using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEvents : MonoBehaviour
{
    ParticleSystem.MainModule particle;
    ParticleSystem mainParticle;
    private void Awake()
    {
        mainParticle = GetComponent<ParticleSystem>();
        particle = mainParticle.main;
        particle.stopAction = ParticleSystemStopAction.Disable;
    }

}
