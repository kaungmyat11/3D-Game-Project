using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameController : MonoBehaviour
{

    public AudioSource clickSource;
    public AudioClip clickClip;

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSound()
    {
        clickSource.PlayOneShot(clickClip);
    }

    public void displayPausePanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void ExitClick()
    {
        SceneManager.LoadScene(0);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
}
