using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    public float forwardSpeed = 1;
    public float lateralRotation = 1;
    public float jumpForce = 1;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        rb.AddRelativeTorque(transform.forward * Input.GetAxis("Vertical") * forwardSpeed * Time.deltaTime);
        rb.AddRelativeTorque(transform.right * Input.GetAxis("Horizontal") * lateralRotation * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector3.up * jumpForce;
        }
    }
}
