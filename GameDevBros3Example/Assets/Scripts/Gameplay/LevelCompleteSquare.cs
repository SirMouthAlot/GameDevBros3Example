using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteSquare : MonoBehaviour
{
    [SerializeField] List<Sprite> powerupList;
    [SerializeField] GameObject gameManager;

    float changeTimer = 0;

    int previousChoice = 5;

    bool itemCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.GetComponent<ScoreCounter>().AddScore(gameManager.GetComponent<Timer>().GetTimeLeft() * 100);

        gameManager.GetComponent<LevelStatus>().SetLevelComplete(true);

        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("LevelClear");

        itemCollected = true;
    }

    void Update()
    {
        if (!itemCollected)
        {
            ChangeImage();
        }
    }

    void ChangeImage()
    {
        if (changeTimer < Time.realtimeSinceStartup)
        {
            int choice = (int)Random.Range(0, 5);

            if (choice == 5)
            {
                choice = 4;
            }

            if (choice == previousChoice)
            {
                choice++;
            }

            GetComponent<SpriteRenderer>().sprite = powerupList[choice];

            changeTimer = Time.realtimeSinceStartup + 0.25f;

            previousChoice = choice;
        }
    }
}
