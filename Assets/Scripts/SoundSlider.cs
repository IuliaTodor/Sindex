using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private GameObject soundManager;
    private AudioSource[] soundSources;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        
    }
    void Start()
    {
        GameObject.Find("Toggle").GetComponent<Toggle>().isOn = Convert.ToBoolean(PlayerPrefs.GetInt("musicToogle")); //cambia el toggle de musica a su estado actual
        soundManager = GameObject.Find("SoundManager");

        soundSources = soundManager.GetComponents<AudioSource>();
        volumeSlider = GetComponent<Slider>();
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.8f);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        //AudioListener.volume = volumeSlider.value;
        switch (name)
        {
            case "SliderMusic":
                soundSources[0].volume = volumeSlider.value;
                break;
            case "SliderSFX":
                soundSources[1].volume = volumeSlider.value;
                break;          
            default:
                break;
        }

        //Save();
    }
    private void Load()
    {
        switch (name)
        {
            case "SliderMusic":
                volumeSlider.value = soundSources[0].volume;
                break;
            case "SliderSFX":
                volumeSlider.value = soundSources[1].volume;
                break;
            default:
                break;
        }
    }
    //private void Save()
    //{
    //    switch (name)
    //    {
    //        case "SliderMusic":
    //            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    //            break;
    //        case "SliderSFX":
    //            PlayerPrefs.SetFloat("sfxVolume", volumeSlider.value);
    //            break;
    //        default:
    //            break;
    //    }

    //}

}
