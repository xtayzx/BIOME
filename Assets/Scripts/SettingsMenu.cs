using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeMusic (float volume) {
        audioMixer.SetFloat("MusicVolume", volume*10);
    }

    public void SetVolumeFX (float volume) {
        audioMixer.SetFloat("FXVolume", volume*10);
    }
}
