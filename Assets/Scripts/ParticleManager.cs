using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public CameraShake playerCamera;
    public Particle[] particles;

    public void Instantiate(string name, Vector3 position, Quaternion rotation, Color color, bool camShake)
    {
        Particle ps = Array.Find(particles, particle => particle.name == name);
        if (ps == null)
        {
            Debug.LogWarning("Particle system: " + name + "not found!");
            return;
        }

        Vector3 instancePosition = new Vector3(
            position.x,
            position.y,
            position.z
        );

        GameObject explosion = Instantiate(ps.prefab, instancePosition, rotation);

        if (color != Color.clear)
        {
            Renderer coloredPart = explosion.GetComponent<ParticleSystemHierarchy>().coloredPart;
            coloredPart.material.color = color;
        }

        FindObjectOfType<AudioManager>().Play(ps.soundName);

        if (camShake) {
            StartCoroutine(playerCamera.Shake(ps.shakeMagnitude, ps.shakeDuration));
        }
    }
}
