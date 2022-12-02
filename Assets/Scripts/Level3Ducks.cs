using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Level3Ducks : MonoBehaviour
{
    private int ducksCollected = 0;
    private bool completedTask = false;
    private bool oilCleaned = false;
    private int oilPuddlesCleaned = 0;

    private bool restartLevel3 = false;

    [SerializeField] private GameObject duck1;
    [SerializeField] private GameObject duck2;
    [SerializeField] private GameObject duck3;

    [SerializeField] private GameObject quack1;
    [SerializeField] private GameObject quack2;
    [SerializeField] private GameObject quack3;
    
    void Update() {
        if(oilPuddlesCleaned >= 7) {
            oilCleaned = true;
        }

        if(ducksCollected >= 3 && oilCleaned) {
            completedTask = true;
        }

        if(restartLevel3 == true) {
            duck1.SetActive(true);
            duck1.GetComponent<NPC>().ChangeCompletedGoal(false);
            duck1.GetComponent<NPC>().TriggerActiveState(false);
            duck1.GetComponent<NPC>().TriggerCompletedState(false);
            duck1.GetComponent<NPC>().ResetActionIcon();

            duck2.SetActive(true);
            duck2.GetComponent<NPC>().ChangeCompletedGoal(false);
            duck2.GetComponent<NPC>().TriggerActiveState(false);
            duck2.GetComponent<NPC>().TriggerCompletedState(false);
            duck2.GetComponent<NPC>().ResetActionIcon();

            duck3.SetActive(true);
            duck3.GetComponent<NPC>().ChangeCompletedGoal(false);
            duck3.GetComponent<NPC>().TriggerActiveState(false);
            duck3.GetComponent<NPC>().TriggerCompletedState(false);
            duck3.GetComponent<NPC>().ResetActionIcon();

            quack1.GetComponent<DuckQuack>().ChangeDuckSaved(false);
            quack2.GetComponent<DuckQuack>().ChangeDuckSaved(false);
            quack3.GetComponent<DuckQuack>().ChangeDuckSaved(false);

            ducksCollected = 0;
            restartLevel3 = false;
        }
    }

    public void Level3Reset(bool state) {
        restartLevel3 = state;
    }

    public void OnTriggerEnter(Collider exit) {
        if (exit.CompareTag("Player") && completedTask == true)
            {
              FindObjectOfType<LevelManager>().CompleteLevel();
            }
    }
    
    public void AddDuck() {
        ducksCollected++;
    }

    public bool DucksAllSaved() {
        return completedTask;
    }

    public void AddOilsCleaned() {
        oilPuddlesCleaned++;
    }
}
