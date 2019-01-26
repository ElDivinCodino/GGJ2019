using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateLeaderboard : MonoBehaviour
{
    public GameObject leaderboard;
    private string lead;

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        lead = leaderboard.GetComponent<LeaderboardTestGUI>().fillLeaderboard();
        Debug.Log(lead);
        this.GetComponent<Text>().text = lead;
    }

}
