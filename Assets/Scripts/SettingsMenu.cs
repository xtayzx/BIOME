using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // Changing the volume of the game and for future, to set the size of the screen during gameplay
    // TODO - hold the value of the level with the slider if opening up again
    
    public AudioMixer audioMixer;

    public void SetVolumeMusic (float volume) {
        audioMixer.SetFloat("MusicVolume", volume*10);
    }

    public void SetVolumeFX (float volume) {
        audioMixer.SetFloat("FXVolume", volume*10);
    }
}
