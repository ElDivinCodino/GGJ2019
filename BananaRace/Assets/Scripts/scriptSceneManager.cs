using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneId)
    {
        // Load the level named "HighScore".
        if (SceneManager.GetActiveScene().buildIndex != 1 || SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine(this.GetComponent<AudioManager>().FadeOut(0, 1));
            this.GetComponentInChildren<countdown>().enabled = true;
            this.GetComponentInChildren<Timer>().enabled = true;
            //GameObject.Find("counter").gameObject.active = true;
        }
        else
        {
            this.GetComponentInChildren<countdown>().enabled = false;
            this.GetComponentInChildren<Timer>().enabled = false;
        }

        SceneManager.LoadScene(sceneId);



    }

    public void Quit()
    {
        Application.Quit();
    }
}
