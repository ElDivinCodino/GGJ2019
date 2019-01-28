using System.Collections;
using UnityEngine;
using GamepadInput;
using TMPro;

public class BananaMovement : MonoBehaviour
{
    public Transform rocketSx, rocketDx, rocketCentral;
    public float enginePower, maxSpeed, SpeedUpDuration, SpeedUpMultiplier, rotationSpeed, jumpForce;
    public ParticleSystem particleSx, particleDx;
    public GamePad.Index playerIndex;
    public AudioSource audioSourceHit;
    public AudioSource audioSourceJump;
    
    private float originalSpeed, originalMaxForce;
    private Rigidbody rb;
    private bool isJumping = false;
    TextMeshProUGUI textSpeedP1;
    TextMeshProUGUI textSpeedP2;
    PositionManager positionManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionManager = GetComponent<PositionManager>();
        textSpeedP1 = GameObject.Find("CanvasP1").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textSpeedP2 = GameObject.Find("CanvasP2").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originalSpeed = enginePower;
        originalMaxForce = maxSpeed;
    }

    void FixedUpdate()
    {
        int currentSpeed = Mathf.FloorToInt(rb.velocity.magnitude);

        if (playerIndex == GamePad.Index.One)
        {
            textSpeedP1.text = Mathf.RoundToInt(rb.velocity.magnitude * 10).ToString() + " Km/h";          
        }
        else
        {
            textSpeedP2.text = Mathf.RoundToInt(rb.velocity.magnitude * 10).ToString() + " Km/h";
        }

        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = 0;
        transform.rotation = Quaternion.Euler(currentRotation);

        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, 0.25f);
                
        if (isGrounded && currentSpeed < maxSpeed)
        {
            float vertical = vertical = playerIndex == GamePad.Index.One ? -Input.GetAxis("Vertical_P1") : -Input.GetAxis("Vertical_P2");

            if (vertical < 0)
            {
                vertical /= 2;
            }

            rb.AddForceAtPosition(transform.forward * vertical * enginePower * 200 * Time.fixedDeltaTime, rocketCentral.position, ForceMode.Acceleration);
        }

        if (isJumping && isGrounded) { isJumping = false; }

        float horizontal = playerIndex == GamePad.Index.One ? Input.GetAxis("Horizontal_P1") : Input.GetAxis("Horizontal_P2");
        float currentRotationSpeed = rotationSpeed * horizontal;
        float finalRotation = currentRotationSpeed * (1 - (currentSpeed / maxSpeed));

        Vector3 rotationTorque = Vector3.up * currentRotationSpeed * Time.fixedDeltaTime;
        rb.AddTorque(rotationTorque, ForceMode.VelocityChange);   

        if (GamePad.GetButtonDown(GamePad.Button.Y, playerIndex))
        {
            positionManager.ResetPosition();
        }
        
        if (!isJumping && isGrounded && GamePad.GetButtonDown(GamePad.Button.A, playerIndex)) //|| (playerIndex == GamePad.Index.One ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.Keypad0)) && canJump))
        {
            rb.velocity += Vector3.up * jumpForce;
            audioSourceJump.Play();
            isJumping = true;
        }
    }

    public void SpeedUp()
    {
        particleDx.Play();
        particleSx.Play();
        enginePower *= SpeedUpMultiplier;
        maxSpeed *= SpeedUpMultiplier;
        StartCoroutine(DeactivateSpeedUp(SpeedUpDuration));
    }

    IEnumerator DeactivateSpeedUp(float SpeedUpDuration)
    {
        yield return new WaitForSeconds(SpeedUpDuration);
        particleDx.Stop();
        particleSx.Stop();
        enginePower = originalSpeed;
        maxSpeed = originalMaxForce;
    }

    public void OnCollisionEnter(Collision collision)
    {
        audioSourceHit.Play();
    }
}
