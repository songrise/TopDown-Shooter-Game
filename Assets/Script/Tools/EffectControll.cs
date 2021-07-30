using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;
public class EffectControl
{
    /*
    This tool class is used to control all the effects of the game.
    the effects include:
    - Glitch effect on screen
    - Light effect 
    - Sound effect
    - Particle effect
    - Outline effect
    */

    private static EffectControl instance;
    private Camera mainCamera;
    private AnalogGlitch analogGlitch;

    private AudioSource bgm;
    private EffectControl()
    {
        mainCamera = Camera.main;
        bgm = mainCamera.GetComponent<AudioSource>();//temp at camera
        analogGlitch = mainCamera.GetComponent<AnalogGlitch>();
    }

    public static EffectControl GetInstance()
    {
        if (instance == null)
        {
            instance = new EffectControl();
        }
        return instance;
    }


    public void enterSlowModeEffect()
    {
        addAnalogJitter();
    }
    public void exitSlowModeEffect()
    {
        removeAnalogJitter();
    }

    private void addAnalogJitter()
    {
        bgm.pitch = 0.5f;
        analogGlitch.enabled = true;
    }

    private void removeAnalogJitter()
    {
        bgm.pitch = 1f;
        analogGlitch.enabled = false;
    }
}