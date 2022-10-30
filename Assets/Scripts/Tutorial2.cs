using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jump
public class Tutorial2 : MonoBehaviour
{
    public Animator controls;

    public void ShowJump() {
        controls.SetBool("ShowJump", true);
    }

    public void HideJump() {
        controls.SetBool("ShowJump", false);
    }
}
