using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerLabel;
    public float startTimer = 0;

    public float time;

    void Update()
    {


        if (startTimer > 0)
        {
            time += Time.deltaTime;

            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;

            //update the label value
            timerLabel.text = string.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
        }
        else
        {
            //do nothing
        }
    }



}