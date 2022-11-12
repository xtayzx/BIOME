using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudioManager : MonoBehaviour
{
    // Only one AudioManager for the whole game
    // TODO: Figure out a way to combine both DontDestory Game and Audio Manager
    public static DontDestroyAudioManager thisObject;

    void Awake() {
        DontDestroyOnLoad(this);

        if (thisObject == null) {
            thisObject = this;
        } else {
            Destroy(gameObject);
        }
    }

}
