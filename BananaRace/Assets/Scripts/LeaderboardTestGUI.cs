using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: show leaderborad only if score is above current level

//isHighscore = Leaderboard.CheckScore(playerScore);
 
//if(isHighscore)
// Do something like activate a UI input field to capture player name

public class LeaderboardTestGUI : MonoBehaviour
{
    private string _nameInput = "";
    private string _scoreInput = "0";

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

        // Display high scores!
        for (int i = 0; i < Leaderboard.EntryCount; ++i)
        {
            var entry = Leaderboard.GetEntry(i);
            GUILayout.Label("Name: " + entry.name + ", Score: " + entry.score);
        }

        // Interface for reporting test scores.
        GUILayout.Space(10);

        _nameInput = GUILayout.TextField(_nameInput);
        _scoreInput = GUILayout.TextField(_scoreInput);

        if (GUILayout.Button("Record"))
        {
            int score;
            int.TryParse(_scoreInput, out score);

            Leaderboard.Record(_nameInput, score);

            // Reset for next input.
            _nameInput = "";
            _scoreInput = "0";
        }

        if (GUILayout.Button("Clear"))
        {
            Leaderboard.Clear();
        }

        GUILayout.EndArea();
    }

    public void clearLeaderboard()
    {
        Leaderboard.Clear();
    }
}