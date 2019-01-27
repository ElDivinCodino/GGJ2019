using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        int numGM = FindObjectsOfType<GameManager>().Length;

        if (numGM > 1)
        {
            GameObject obj = FindObjectsOfType<GameManager>()[0].gameObject;
            Destroy(obj);
        }

        DontDestroyOnLoad(this);

        audioManager = GetComponent<AudioManager>();
        GameObject.Find("counter").gameObject.active = false;
    }

    void Start()
    {
        audioManager.PlayMusicInMenu();
    }
}
