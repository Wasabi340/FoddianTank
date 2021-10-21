using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticles : MonoBehaviour
{
    public void AboutToDie()
    {
        transform.parent = null;
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(1f);
        if (transform.GetComponent<ParticleSystem>().particleCount == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(DeathTimer());
        }
    }
}
