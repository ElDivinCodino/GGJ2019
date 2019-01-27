using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimerOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "BananaP1" || collision.gameObject.name == "BananaP2")
        {
            GameObject.Find("TimeChrono").GetComponent<ControlTimer>().StopTimer();
        }

    }

}
