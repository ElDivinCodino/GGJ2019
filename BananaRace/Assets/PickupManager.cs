using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public float rotatingSpeed;
    public int numberOfPowerUps;
    Vector3 rotatingVector;
    AudioManager audioManager;

    void Start()
    {
        rotatingVector = new Vector3(Random.Range(15, 45), Random.Range(15, 45), Random.Range(15, 45));
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        transform.Rotate(rotatingVector * rotatingSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.gameObject.name.Contains("Banana"))
        {
            other.transform.parent.gameObject.GetComponent<PowerUp>().SelectPowerUp();
            audioManager.PlayStartBeep();
            GameObject.Destroy(gameObject);
        }
    }
}
