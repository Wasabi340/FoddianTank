using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private int layerMask = 1 << 3;
    private int maxBounces = 99;
    private int currentBounce = 0;
    private Vector3[] trajectory;
    private bool alive = true;

    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        trajectory = FindTrajectory();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //for (int i = 1; i < trajectory.Length; i++)
        //{
        //    Debug.DrawLine(trajectory[i - 1], trajectory[i]);
        //}

        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bounce"))
        {
            if (currentBounce == trajectory.Length - 1)
            {
                Explode();
                FindObjectOfType<AudioManager>().Play("BulletExplode");
            }
            else
            {
                transform.position = trajectory[currentBounce];
                transform.rotation = Quaternion.LookRotation(trajectory[currentBounce + 1] - transform.position);
                currentBounce++;
                FindObjectOfType<AudioManager>().Play("BulletBounce");
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Explode(true);
            Explode();
        }
    }

    private Vector3[] FindTrajectory()
    {

        List<Vector3> targets = new List<Vector3>();

        RaycastHit hit;

        Vector3 currentPosition = transform.position;
        Vector3 currentDirection = transform.forward;

        int calculatedHit = 0;

        while (Physics.Raycast(currentPosition, currentDirection, out hit, Mathf.Infinity, layerMask) && calculatedHit < maxBounces + 1)
        {
            targets.Add(hit.point);
            currentPosition = hit.point;
            currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            calculatedHit++;
        }

        return targets.ToArray();
    }

    public void Explode()
    {
        FindObjectOfType<ParticleManager>().Instantiate(
            "BulletExplosion",
            transform.position,
            Quaternion.identity,
            Color.clear,
            false
        );
        Destroy(gameObject);
    }
}
