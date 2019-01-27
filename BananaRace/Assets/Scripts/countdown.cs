using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countdown : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        //Debug.Log(GameObject.Find("counter").name);
        //GameObject.Find("counter").gameObject.active = true;
        GameObject.Find("counter").GetComponent<TextMeshProUGUI>().enabled = true;
        StartCoroutine(Countdown(4));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            if (count < 4)
            {
                GameObject.Find("GameManager").GetComponent<AudioManager>().PlayStartBeep();
                text.GetComponent<TextMeshProUGUI>().text = count.ToString();
            }

            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        StartCoroutine(StartGame(1));
    }

    IEnumerator StartGame(int seconds)
    {
        // display go
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayStartBeep();
        text.GetComponent<TextMeshProUGUI>().text = "GO!";

        // activate banana movement and audio collision
        GameObject B1 = GameObject.Find("BananaP1");
        B1.GetComponent<BananaMovement>().enabled = true;
        var A1 = B1.GetComponents<AudioSource>();
        A1[0].enabled = true;

        GameObject B2 = GameObject.Find("BananaP2");
        if (B2 != null)
        {
            B2.GetComponent<BananaMovement>().enabled = true;
            var A2 = B2.GetComponents<AudioSource>();
            A2[0].enabled = true;
        }

        //start music
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayMusicInGame();

        yield return new WaitForSeconds(1);

        //start timer
        GameObject.Find("Chrono Text").GetComponent<TextMeshProUGUI>().text = string.Format("{0:00} : {1:00} : {2:000}", 0, 0, 0);
        GameObject.Find("TimeChrono").GetComponent<ControlTimer>().StartRace();

        text.SetActive(false);
    }
}
