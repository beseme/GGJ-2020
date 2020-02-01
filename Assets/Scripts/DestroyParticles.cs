using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour, IUpdate
{
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        UpdateManager.updateManager.register(this);
        this.particles = this.GetComponent<ParticleSystem>();
    }

    public void DoUpdate()
    {
        if (this.particles && !this.particles.IsAlive())
        {
            Destroy(this.gameObject);
            UpdateManager.updateManager.unregister(this);
        }
    }
}
