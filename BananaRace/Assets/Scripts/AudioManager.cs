using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip musicInGame;
    public AudioClip musicInMenu;
    public AudioClip startRaceBeep;
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSFX;

    public void PlayMusicInMenu()
    {
        if (musicInMenu)
        {
            audioSourceMusic.clip = musicInMenu;
            audioSourceMusic.Play();
        }
    }

    public void PlayMusicInGame()
    {
        if (musicInGame)
        {
            audioSourceMusic.clip = musicInGame;
            audioSourceMusic.Play();
        }
    }

    public void PlayStartBeep()
    {
        audioSourceSFX.clip = startRaceBeep;
        audioSourceSFX.Play();
    }

    public IEnumerator FadeOut(int sourceId, float FadeTime)
    {
        if (sourceId == 0)
        {
            float startVolume = audioSourceMusic.volume;

            while (audioSourceMusic.volume > 0)
            {
                audioSourceMusic.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSourceMusic.Stop();
            audioSourceMusic.volume = startVolume;

        } else
        {

        }
    }

}
