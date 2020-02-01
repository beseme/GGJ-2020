using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        this.particles = this.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (this.particles && !this.particles.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
