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
        }
    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, boccaDiFuoco.position + transform.forward, boccaDiFuoco.rotation);
        bulletInstance.GetComponent<Rigidbody>().velocity = bulletForce * boccaDiFuoco.forward;
    }
}
