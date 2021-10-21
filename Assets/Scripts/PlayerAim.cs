using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject head;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gameIsPaused) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
            {
                Transform objectHit = hit.transform;
            }

            Vector3 lookDirection = hit.point - head.transform.position;
            lookDirection = lookDirection.normalized;
            lookDirection.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            head.transform.rotation = lookRotation;
        }
    }
}
