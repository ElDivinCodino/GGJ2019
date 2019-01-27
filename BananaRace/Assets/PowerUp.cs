using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PowerUp : MonoBehaviour
{
    enum PowerupType { NONE, SpeedUp, Shoot }
    public GameObject bullet;
    public Transform boccaDiFuoco;
    public float bulletForce;

    GamePad.Index playerIndex;
    PowerupType currentPowerUp = PowerupType.NONE;

    void Start()
    {
        playerIndex = GetComponent<BananaMovement>().playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPowerUp != PowerupType.NONE && (GamePad.GetButtonDown(GamePad.Button.X, playerIndex) /*|| playerIndex == GamePad.Index.One ? Input.GetKeyDown(KeyCode.H) : Input.GetKeyDown(KeyCode.Keypad1))*/))
        { 
            switch(currentPowerUp)
            {
                case PowerupType.SpeedUp:            
                    GetComponent<BananaMovement>().SpeedUp();
                    break;
                case PowerupType.Shoot:
                    Shoot();
                    break;
                default:
                    break;
            }

            currentPowerUp = PowerupType.NONE;
        }
    }

    public void SelectPowerUp()
    {
        int num = Random.Range(1, 3);

        switch (num)
        {
            case 1:
                currentPowerUp = PowerupType.SpeedUp;
                break;
            case 2:
                currentPowerUp = PowerupType.Shoot;
                break;
            default:
                break;
        }
        Debug.Log(currentPowerUp);
    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, boccaDiFuoco.position + (boccaDiFuoco.up/0.1f), boccaDiFuoco.rotation);
        bulletInstance.GetComponent<Rigidbody>().velocity = bulletForce * boccaDiFuoco.forward;
    }
}
