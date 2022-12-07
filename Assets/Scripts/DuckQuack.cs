using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckQuack : MonoBehaviour
{
    [SerializeField] private GameObject duck;
    private bool duckSaved;
    
    public void OnTriggerEnter(Collider quack)
    {
        if (quack.CompareTag("Player") && duckSaved == false)
        {
            FindObjectOfType<AudioManager>().Play("DuckQuack");
            Debug.Log("Play duck sound");
            return;
        }
    }

    public void ChangeDuckSaved(bool state) {
        duckSaved = state;
    }

    void Update() {
        if(duck.GetComponent<NPC>().ShowCompleteGoal() == true) {
            duckSaved = true;
        }
    }
}
