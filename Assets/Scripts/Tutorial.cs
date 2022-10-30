using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interaction
public class Tutorial : MonoBehaviour
{
    public Animator controls;

    public void ShowControls() {
        controls.SetBool("IsMoved", true);
    }

    public void HideControls() {
        controls.SetBool("IsMoved", false);
    }
}
