using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    public Transform rocketSx, rocketDx, rocketCentral;
    public float speed, maxForce;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.magnitude) < maxForce)
        {
            rb.AddForceAtPosition(transform.forward * Input.GetAxis("Vertical") * speed * 200 * Time.deltaTime, rocketCentral.position);

            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal > 0)
            {
                transform.RotateAround(transform.position, Vector3.up, 40 * Time.deltaTime);
            }
            else if (horizontal < 0)
            {
                transform.RotateAround(transform.position, Vector3.up, -40 * Time.deltaTime);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector3.up * speed;
        }
    }
}
