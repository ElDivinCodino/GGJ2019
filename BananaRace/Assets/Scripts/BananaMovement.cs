using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class BananaMovement : MonoBehaviour
{
    public Transform rocketSx, rocketDx, rocketCentral;
    public float speed, maxForce, SpeedUpDuration, SpeedUpMultiplier;
    public ParticleSystem particleSx, particleDx;
    public GamePad.Index playerIndex;

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

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxForce)
        {
            float mult = Mathf.Abs(Vector3.Angle(transform.forward, rb.velocity)) < 90 ? 200 : 60;

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

        if ((GamePad.GetButtonDown(GamePad.Button.A, playerIndex) || playerIndex == GamePad.Index.One? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.Keypad0)) && canJump)
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
