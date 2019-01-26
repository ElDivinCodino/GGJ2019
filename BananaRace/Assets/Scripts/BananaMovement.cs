﻿using System.Collections;
using UnityEngine;
using GamepadInput;

public class BananaMovement : MonoBehaviour
{
    public Transform rocketSx, rocketDx, rocketCentral;
    public float speed, maxForce, SpeedUpDuration, SpeedUpMultiplier;
    public ParticleSystem particleSx, particleDx;
    public GamePad.Index playerIndex;
    public AudioSource audioSourceHit;
    public AudioSource audioSourceJump;

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

            float vertical = (playerIndex == GamePad.Index.One ? Input.GetAxis("Vertical_P1") : Input.GetAxis("Vertical_P2"));
            float horizontal = playerIndex == GamePad.Index.One ? Input.GetAxis("Horizontal_P1") : Input.GetAxis("Horizontal_P2");

            rb.AddForceAtPosition(transform.forward * vertical * speed * mult * Time.fixedDeltaTime, rocketCentral.position);

            if (horizontal > 0)
            {
                transform.RotateAround(transform.position, Vector3.up, 40 * Time.fixedDeltaTime);
            }
            else if (horizontal < 0)
            {
                transform.RotateAround(transform.position, Vector3.up, -40 * Time.fixedDeltaTime);
            }
        }

        if (GamePad.GetButtonDown(GamePad.Button.X, playerIndex) || playerIndex == GamePad.Index.One ? Input.GetKeyDown(KeyCode.H) : Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpeedUp();
        }

        if ((GamePad.GetButtonDown(GamePad.Button.A, playerIndex) || (playerIndex == GamePad.Index.One ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.Keypad0)) && canJump))
        {
            audioSourceJump.Play();
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

    public void OnCollisionEnter(Collision collision)
    {
        audioSourceHit.Play();
    }
}
