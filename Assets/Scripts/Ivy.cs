using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivy : MonoBehaviour
{
    public void OnTriggerEnter(Collider ivy)
    {
        if (ivy.CompareTag("Player"))
        {
            FindObjectOfType<PauseMenu>().FallResetGame();
        }
    }
}
