using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameHeight : MonoBehaviour
{
    public GameObject target;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.y - target.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + offset, transform.position.z);
    }
}
