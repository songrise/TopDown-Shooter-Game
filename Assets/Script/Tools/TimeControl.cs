using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl
{
    // Start is called before the first frame update

    private TimeControl()
    {
    }

    public static TimeControl GetInstance()
    {
        if (instance == null)
        {
            instance = new TimeControl();
        }
        return instance;
    }
    private static TimeControl instance;
    private float fixedDeltaTime = 0.02f; //hardcoded for now



    public void slowDownGame(float scale)
    {
        float t = 1.0f / scale;
        Debug.Log("slow down game by " + t);
        Time.timeScale = t;
        EffectControl.GetInstance().enterSlowModeEffect();
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void revertGameSpeed()
    {
        Debug.Log("revert game speed");
        Time.timeScale = 1.0f;
        EffectControl.GetInstance().exitSlowModeEffect();
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }


}