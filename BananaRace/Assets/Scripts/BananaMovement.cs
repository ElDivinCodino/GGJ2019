using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    public Transform rocketSx, rocketDx, rocketCentral;
    public float speed, maxForce, SpeedUpDuration, SpeedUpMultiplier;
    public ParticleSystem particleSx, particleDx;

    private float originalSpeed, originalMaxForce;
    private Rigidbody rb;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;
        originalMaxForce = maxForce;
        canJump = true;
    }

    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;

        transform.rotation = Quaternion.Euler(rot);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxForce)
        {
            float mult = Mathf.Clamp(0, 200, Mathf.Abs(Vector3.Angle(transform.right, rb.velocity)) * 200 / 90);
            rb.AddForceAtPosition(transform.forward * Input.GetAxis("Vertical") * speed * mult * Time.fixedDeltaTime, rocketCentral.position);

            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal > 0)
            {
                transform.RotateAround(transform.position, Vector3.up, 40 * Time.fixedDeltaTime);
            }
            else if (horizontal < 0)
            {
                transform.RotateAround(transform.position, Vector3.up, -40 * Time.fixedDeltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpeedUp();
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity += Vector3.up * speed;
        }
    }

    public void SpeedUp()
    {
        particleDx.Play();
        particleSx.Play();
        speed *= SpeedUpMultiplier;
        maxForce *= SpeedUpMultiplier;
        StartCoroutine(DeactivateSpeedUp(SpeedUpDuration));
    }

    IEnumerator DeactivateSpeedUp(float SpeedUpDuration)
    {
        yield return new WaitForSeconds(SpeedUpDuration);
        particleDx.Stop();
        particleSx.Stop();
        speed = originalSpeed;
        maxForce = originalMaxForce;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Do stuff
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
