using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    public Slider volumeSlider;

    [SerializeField]
    public Slider soundEffectSlider;

    [SerializeField]
    public AudioMixer audioMixer;


    void Start()
    {
        if(PlayerPrefs.HasKey("GameVolume") && PlayerPrefs.HasKey("SoundEffectVolume")) {
            LoadVolumePreferences();
            SetMusicVolume();
        } else {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume() {
        //it appears that Audio Mixer uses a log(10) scale in its value
        //We need to normalize it to make it work better
        audioMixer.SetFloat("Master Volume", Mathf.Log10(volumeSlider.value) * 15);
        audioMixer.SetFloat("FX Volume", Mathf.Log10(soundEffectSlider.value) * 15);

        //Save the current settings for later use
        SaveVolumePreferences();
    }

    private void LoadVolumePreferences() {
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume");
        soundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");
    }

    private void SaveVolumePreferences() {
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("SoundEffectVolume", soundEffectSlider.value);
    }
}
