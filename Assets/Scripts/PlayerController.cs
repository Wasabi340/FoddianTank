using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject body;
    public GameObject head;
    public Transform shootPosition;
    public GameObject bulletPrefab;
    public float moveSpeed = 0f;
    public float maxSpeed = 2.5f;
    public float acceleration = 15f;
    public float deceleration = 10f;

    public bool falling = false;
    public bool active = true;

    public Transform spawn;
    public GameObject bulletContainer;

    private bool[] inputs;
    private bool shoot;
    private Rigidbody rb;
    public Renderer colorRenderer;

    public ProgressCalculator progress;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new bool[4];
        shoot = false;
    }

    public void Update() { 
        shoot = (shoot || Input.GetMouseButtonDown(0)) && !falling && moveSpeed == 0;
    }

    public void FixedUpdate()
    {
        Vector3 _inputDirection = Vector3.zero;

        inputs[0] = Input.GetKey(KeyCode.W);
        inputs[1] = Input.GetKey(KeyCode.A);
        inputs[2] = Input.GetKey(KeyCode.S);
        inputs[3] = Input.GetKey(KeyCode.D);

        if (inputs[0])
        {
            _inputDirection += Vector3.forward;
        }
        if (inputs[1])
        {
            _inputDirection += -Vector3.right;
        }
        if (inputs[2])
        {
            _inputDirection += -Vector3.forward;
        }
        if (inputs[3])
        {
            _inputDirection += Vector3.right;
        }

        Move(_inputDirection, shoot);
    }
    private void Move(Vector3 _inputDirection, bool _shoot)
    {
        Vector3 direction = Vector3.zero;//_inputDirection;

        if (shoot)
        {
            Shoot();
            //moveSpeed = moveSpeed + acceleration * Time.fixedDeltaTime;
            //if (moveSpeed > maxSpeed)
            //{
            //    moveSpeed = maxSpeed;
            //}

            float power = -Vector3.Dot(head.transform.forward, body.transform.forward) / body.transform.forward.magnitude;

            if (power < 0) {
                body.transform.forward *= -1;
                power *= -1;
            }

            moveSpeed = maxSpeed * power;
            shoot = false;
        }
        else
        {
            moveSpeed = moveSpeed - deceleration * Time.fixedDeltaTime;
            if (moveSpeed < 0)
            {
                moveSpeed = 0;
            }
        }

        if (_inputDirection != Vector3.zero && moveSpeed == 0)
        {

            if (Mathf.Abs(Vector3.Angle(body.transform.forward, _inputDirection)) > 110f)
            {
                body.transform.forward *= -1;
                //moveSpeed *= -1;
            }

            float singleStep = 10f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(body.transform.forward, _inputDirection, singleStep, 0.0f);
            body.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        Vector3 newVelocity = body.transform.forward * moveSpeed;

        rb.velocity = new Vector3(newVelocity.x, Mathf.Max(rb.velocity.y, -20), newVelocity.z);

    }

    private void Shoot() {
        progress.UpdateProjectileCount();
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, head.transform.rotation);
        bullet.transform.parent = bulletContainer.transform;
        FindObjectOfType<AudioManager>().Play("Shoot");
    }

    public void Explode(bool _bad) {
        moveSpeed = 0;
        shoot = false;

        if (_bad)
        {
            progress.UpdateDeathCount();
        }

        FindObjectOfType<ParticleManager>().Instantiate(
            "TankExplosion",
            transform.position + Vector3.up,
            Quaternion.identity,
            colorRenderer.material.color,
            _bad
        );

        foreach (Transform child in bulletContainer.transform)
        {
            child.GetComponent<Bullet>().Explode();
        }
        FindObjectOfType<AudioManager>().Play("BulletExplode");
        transform.position = spawn.position;
        body.transform.forward = Vector3.forward;
    }
}
