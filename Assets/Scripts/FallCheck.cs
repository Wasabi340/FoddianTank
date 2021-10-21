using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    Rigidbody rb;
    PlayerController pc;
    Renderer rd;

    public GameObject body;

    public Material normal, falling;

    public GameObject bulletContainer;

    private int layerMask = 1 << 11;

    public LevelController levelController;

    public ProgressCalculator progress;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
        rd = body.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position + transform.up, Vector3.down, out hit, 0.55f, layerMask)) {
            if (pc.active) {
                pc.active = false;
                rd.material = falling;
                foreach (Transform child in bulletContainer.transform)
                {
                    child.GetComponent<Bullet>().Explode();
                }
                progress.UpdateFallCount();
            }
        }
        else {
            if (!pc.active && pc.falling) {
                pc.active = true;
                rb.constraints |= RigidbodyConstraints.FreezePositionY;
                transform.position = new Vector3(transform.position.x, hit.transform.position.y, transform.position.z);
                pc.falling = false;
                rd.material = normal;
                if (hit.transform.gameObject.name == "Ground")
                {
                    pc.Explode(true);
                }
                levelController.SetEnd();
            }
        }
        if (!pc.active && pc.moveSpeed == 0 && !pc.falling)
        {
            levelController.DisableCurrentLevel();
            pc.falling = true;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
}
