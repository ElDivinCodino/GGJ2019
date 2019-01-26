using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countdown : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            text.GetComponent<AudioSource>().Play();
            text.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        StartCoroutine(StartGame(1));
    }

    IEnumerator StartGame(int seconds)
    {
        // do something...
        text.GetComponent<AudioSource>().Play();
        text.GetComponent<TextMeshProUGUI>().text = "GO!";
        yield return new WaitForSeconds(1);
        text.SetActive(false);
    }
}
