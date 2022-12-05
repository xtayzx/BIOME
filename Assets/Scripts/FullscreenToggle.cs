using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
   public void Fullscene(bool state) {
        Screen.fullScreen = state;
   }
}
