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
    private string text;

    public string fillLeaderboard()
    {
        text = "";
        for (int i = 0; i < Leaderboard.EntryCount; ++i)
        {
            var entry = Leaderboard.GetEntry(i);
            text += "Name: " + entry.name + ", Score: " + entry.score +"\n";
        }

        return text;
    }

    public void insertName(string playerName)
    {
        _nameInput = playerName;
        _scoreInput = 1000.ToString(); //TODO: insert score here dynamically
    }

    public void clearLeaderboard()
    {
        Leaderboard.Clear();
    }

    public void recordHighscore()
    {
        int score;
        int.TryParse(_scoreInput, out score);

        Leaderboard.Record(_nameInput, score);

        // Reset for next input.
        _nameInput = "";
        _scoreInput = "0";

    }
}