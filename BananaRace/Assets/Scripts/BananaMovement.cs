using System.Collections;
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
    private bool canJump, canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;
        originalMaxForce = maxForce;
        canJump = canMove = true;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity))
        {
            if(canJump && Vector3.Distance(hit.point, transform.position) > 0.3f)
            {
                canJump = canMove = false;
            } 
            else if (!canJump && Vector3.Distance(hit.point, transform.position) < 0.3f)
            {
                canJump = canMove = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove && rb.velocity.magnitude < maxForce)
        {
            if (Mathf.Abs(Vector3.Angle(transform.forward, rb.velocity)) < 90)
            {
                float vertical = (playerIndex == GamePad.Index.One ? Input.GetAxis("Vertical_P1") : Input.GetAxis("Vertical_P2"));
                rb.AddForceAtPosition(transform.forward * vertical * speed * 200 * Time.fixedDeltaTime, rocketCentral.position);
            }

            float horizontal = playerIndex == GamePad.Index.One ? Input.GetAxis("Horizontal_P1") : Input.GetAxis("Horizontal_P2");

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
            rb.velocity += Vector3.up * (speed / 10);
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

    public void OnCollisionEnter(Collision collision)
    {
        audioSourceHit.Play();
    }
}
