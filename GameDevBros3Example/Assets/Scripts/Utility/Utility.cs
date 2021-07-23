using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //Increment must be -1 or 1
    public static int WrapAround(int max, int current, int increment)
    {
        int temp = current + increment;

        if (temp >= max)
        {
            //Wrap around to first value
            temp = 0;
        }
        else if (temp < 0)
        {
            //wrap around to last value
            temp = max - 1;
        }

        return temp;
    }
}
