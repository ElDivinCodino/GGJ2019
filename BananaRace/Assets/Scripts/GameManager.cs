using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        int numGM = FindObjectsOfType<GameManager>().Length;

        if (numGM > 1)
        {
            GameObject obj0 = FindObjectsOfType<GameManager>()[0].gameObject;
            GameObject obj1 = FindObjectsOfType<GameManager>()[1].gameObject;
            Debug.Log(obj0.GetComponentInChildren<LeaderboardTestGUI>().newScore);
            Debug.Log(obj1.GetComponentInChildren<LeaderboardTestGUI>().newScore);

            obj1.GetComponentInChildren<LeaderboardTestGUI>().newScore = obj0.GetComponentInChildren<LeaderboardTestGUI>().newScore;
            Debug.Log(obj1.GetComponentInChildren<LeaderboardTestGUI>().newScore);

            Destroy(obj0);

        }

        DontDestroyOnLoad(this);

        audioManager = GetComponent<AudioManager>();
        GameObject.Find("counter").GetComponent<TextMeshProUGUI>().enabled = false;
    }

    void Start()
    {
        audioManager.PlayMusicInMenu();
    }
}
