using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public AudioSource clickSource;
    public AudioClip clickClip;

    public AudioSource loopSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoPlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnClickSound()
    {
        loopSource.Stop();
        clickSource.PlayOneShot(clickClip);
    }
}
