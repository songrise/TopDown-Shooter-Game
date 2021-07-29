using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // Start is called before the first frame update

    private TimeControl()
    {
    }

    private static TimeControl instance;
    private float fixedDeltaTime;


    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    public void slowDownGame(float scale)
    {
        float t = 1.0f / scale;
        Debug.Log("slow down game by " + t);
        Time.timeScale = t;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void revertGameSpeed()
    {
        Debug.Log("revert game speed");
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }


    public static TimeControl GetInstance()
    {
        if (instance == null)
        {
            instance = new TimeControl();
        }
        return instance;
    }
}