using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    float timeToLive;
    // Start is called before the first frame update
    void Update()
    {
        if (!particle.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}
