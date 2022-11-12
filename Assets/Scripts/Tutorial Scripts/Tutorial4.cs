using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jump
public class Tutorial4 : MonoBehaviour
{
    public Animator controls;

    public void ShowInventoryControls() {
        controls.SetBool("IsInventory", true);
    }

    public void HideInventoryControls() {
        controls.SetBool("IsInventory", false);
    }
}
