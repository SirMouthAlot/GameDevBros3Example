using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    bool levelCompleted = false;
    bool levelFailed = false;

    // Update is called once per frame
    void Update()
    {
        if (levelCompleted)
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("LevelClear"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (levelFailed)
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("GameOver"))
            {
                SceneManager.LoadScene("World1-1");
            }
        }
    }

    public void SetLevelComplete(bool complete)
    {
        levelCompleted = complete;
    }

    public void SetLevelFailed(bool failed)
    {
        levelFailed = failed;
    }
}
