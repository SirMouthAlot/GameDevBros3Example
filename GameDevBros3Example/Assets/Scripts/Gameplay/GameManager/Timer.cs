using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timeDisplay;
    [SerializeField] int startTime;

    int timeLeft;

    // Update is called once per frame
    void Update()
    {
        timeLeft = (int)(startTime - Time.realtimeSinceStartup);

        timeDisplay.GetComponent<NumberDisplayDefinition>()._numericValue = timeLeft.ToString();

        if (timeLeft < 1)
        {
            GetComponent<LevelStatus>().SetLevelFailed(true);
        }
    }

    public int GetTimeLeft()
    {
        return timeLeft;
    }
}
