using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTimer : MonoBehaviour
{

    public void SetTimer(int t)
    {
        Timer playerTimer = this.GetComponent<Timer>();
        playerTimer.startTimer = t;
    }

    public void StartRace()
    {
        SetTimer(1);
    }

    public void StopTimer()
    {
        SetTimer(0);
        StartCoroutine(EndRace(1));
    }

    IEnumerator EndRace(int seconds)
    {
        int count = seconds;
        bool highscore = false;

        Timer playerTimer = this.GetComponent<Timer>();
        float score = playerTimer.time;
        GameObject.Find("Leaderboard").GetComponent<LeaderboardTestGUI>().newScore = (int)Mathf.Round(score * 100);

        Debug.Log(score);

        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }

        if (highscore)
        {
            GameObject.Find("GameManager").GetComponent<scriptSceneManager>().LoadScene(1);
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<scriptSceneManager>().LoadScene(0);
        }

    }
}
