using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointText : MonoBehaviour
{
    public Animator checkpoint;

    public void ShowText() {
        checkpoint.SetTrigger("IsTriggered");
    }

    public void HideText() {
        // checkpoint.Se("IsTriggered", false);
    }
}
