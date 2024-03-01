using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool playingMusic = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        PlayerPrefs.SetInt("musicToogle", 1);
    }

    public void ToggleMusic(bool playing)
    {
        if (playing == true) 
        {
            SoundManager.instance.Play("music");
            playingMusic = true;
        } 
        else 
        {
            SoundManager.instance.Stop("music");
            playingMusic = false;
        }
        PlayerPrefs.SetInt("musicToogle", Convert.ToInt32(playingMusic));
    }

    public void PressButton()
    {
        SoundManager.instance.Play("button");
    }
}
