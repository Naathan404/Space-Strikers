using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem mainEngineBoostParticle;
    public ParticleSystem upEngineBoostParticle;
    public ParticleSystem downEngineBoostParticle;

    public static ParticleManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        // Stop all particles
        mainEngineBoostParticle.Stop();
        upEngineBoostParticle.Stop();
        downEngineBoostParticle.Stop();
    }

    public void PlayParticle(ParticleSystem particle)
    {
        if (particle.isStopped)
        {
            particle.Play();
        }
    }

    public void StopParticle(ParticleSystem particle)
    {
        if (particle.isPlaying)
        {
            particle.Stop();
        }
    }
}
