using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour
{
    public Animator controls;

    public void ShowInventoryControls() {
        controls.SetBool("ShowInventoryControls", true);
    }

    public void HideInventoryControls() {
        controls.SetBool("ShowInventoryControls", false);
    }

    public void MoveInventory(bool state) {
        if(state == true) {
            controls.SetBool("InventoryOpen", true);
        }

        else if(state == false) {
            controls.SetBool("InventoryOpen", false);
        }
    }
}
