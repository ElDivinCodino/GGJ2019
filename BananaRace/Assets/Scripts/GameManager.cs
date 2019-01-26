using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    void Start()
    {
        audioManager.PlayMusicInGame();      
    }
}
