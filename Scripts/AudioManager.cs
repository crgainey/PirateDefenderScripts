using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectPref = "SoundEffectPref";
    private int firstPlayInt;
    private float backgroundFloat, soundEffectsFloat;
    [SerializeField] Slider backgroundSlider, soundEffectsSlider;
    [SerializeField] AudioSource backgroundAudio;
    [SerializeField] AudioSource[] soundEffectsAudio;

    bool firstPlay;

    void Awake()
    {
        if (Application.isEditor == false)
        {
            if (PlayerPrefs.GetInt("FirstPlay", 1) == 1)
            {
                firstPlay = true;
                PlayerPrefs.SetInt("FirstPlay", 0);
                PlayerPrefs.Save();
            }
            else
                firstPlay = false;
        }
    }
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            backgroundFloat = .125f;
            soundEffectsFloat = .75f;
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }
    }


    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
    }

    public void SaveEffectsettings()
    {
        PlayerPrefs.SetFloat(SoundEffectPref, soundEffectsSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
            SaveEffectsettings();
        }
    }

    public void UpdateSound()
    {

        backgroundAudio.volume = backgroundSlider.value;
        //soundEffectsAudio.volume = soundEffectsSlider.value;

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }
    }
}
