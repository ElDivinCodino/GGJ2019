using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PowerUp : MonoBehaviour
{
    public GameObject bullet;
    public Transform boccaDiFuoco;
    public float bulletForce;

    GamePad.Index playerIndex;
    string currentPowerUp = "";

    void Start()
    {
        playerIndex = GetComponent<BananaMovement>().playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPowerUp != "" && GamePad.GetButtonDown(GamePad.Button.X, playerIndex) || playerIndex == GamePad.Index.One ? Input.GetKeyDown(KeyCode.H) : Input.GetKeyDown(KeyCode.Keypad1))
        {
            switch (currentPowerUp)
            {
                case "SpeedUp":
                    GetComponent<BananaMovement>().SpeedUp();
                    break;
                case "Shoot":
                    Shoot();
                    break;
                default:
                    break;
            }

            currentPowerUp = "";
        }
    }

    public void SelectPowerUp()
    {
        int num = Random.Range(1, 3);

        switch (num)
        {
            case 1:
                currentPowerUp = "SpeedUp";
                break;
            case 2:
                currentPowerUp = "Shoot";
                break;
            default:
                break;
        }

    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, boccaDiFuoco.position, boccaDiFuoco.rotation);

        bulletInstance.GetComponent<Rigidbody>().velocity = bulletForce * boccaDiFuoco.forward; ;
    }
}
