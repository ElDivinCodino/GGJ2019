using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip musicInGame;
    public AudioClip startRaceBeep;
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSFX;


    public void PlayMusicInGame()
    {
        if (musicInGame)
        {
            audioSourceMusic.clip = musicInGame;
            audioSourceMusic.Play();
        }
    }
}
