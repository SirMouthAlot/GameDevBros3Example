using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    bool levelCompleted = false;
    bool levelFailed = false;

    public string _levelFailedScene;
    public string _levelCompleteScene;
    
    // Update is called once per frame
    void Update()
    {
        if (levelCompleted)
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("LevelClear"))
            {
                SceneManager.LoadScene(_levelCompleteScene);
            }
        }

        if (levelFailed)
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("GameOver"))
            {
                SceneManager.LoadScene(_levelFailedScene);
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
