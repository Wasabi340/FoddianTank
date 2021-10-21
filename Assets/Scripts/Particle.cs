using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    public string name;
    public GameObject prefab;
    public string soundName;

    public float shakeMagnitude;
    public float shakeDuration;
}
