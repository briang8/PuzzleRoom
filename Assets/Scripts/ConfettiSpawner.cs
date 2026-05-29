using UnityEngine;

public class ConfettiSpawner : MonoBehaviour
{
    [Header("Confetti Particles")]
    public ParticleSystem[] confettiSystems; // drag all your particle systems here

    public void Play()
    {
        foreach (ParticleSystem ps in confettiSystems)
        {
            if (ps != null) ps.Play();
        }
    }

    public void Stop()
    {
        foreach (ParticleSystem ps in confettiSystems)
        {
            if (ps != null) ps.Stop();
        }
    }
}