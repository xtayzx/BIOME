using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudioManager : MonoBehaviour
{
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
