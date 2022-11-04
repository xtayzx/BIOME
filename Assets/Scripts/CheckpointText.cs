using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointText : MonoBehaviour
{
    // For the UI Text telling the player they have crossed a checkpoint
    public Animator checkpoint;

    public void ShowText() {
        checkpoint.SetTrigger("IsTriggered");
    }

    public void HideText() {
        // checkpoint.Se("IsTriggered", false);
    }
}
