using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteSquare : MonoBehaviour
{
    [SerializeField] List<Sprite> powerupList;
    [SerializeField] GameObject gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.GetComponent<ScoreCounter>().AddScore(gameManager.GetComponent<Timer>().GetTimeLeft() * 100);

        gameManager.GetComponent<LevelStatus>().SetLevelComplete(true);

        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("LevelClear");
    }
}
