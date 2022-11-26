using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelSelectionTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject icon;

    PlayerControls controls;

    [SerializeField] private int levelNum;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Interact.performed += ctx => Interact();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Update()
    {
        if(FindObjectOfType<GameManager>().IsGamePaused() == false) {
            FindObjectOfType<LevelText>().LoadText();
        }
        //Keyboard Action
        if (triggerActive && Input.GetKeyDown(KeyCode.J))
        {
            Interact();
        }
    }

    // If the player enters the finish area, then end the level
    public void OnTriggerEnter(Collider level) {
        if (level.CompareTag("Player"))
            {
                FindObjectOfType<AudioManager>().Play("Object");
                triggerActive = true;
                icon.SetActive(true);

                FindObjectOfType<Player>().SelectedLevel(levelNum);
                FindObjectOfType<LevelText>().ShowText();
                
                if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                    FindObjectOfType<Tutorial>().ShowControls();
                }
            }
    }

    public void OnTriggerExit(Collider level)
    {
        if (level.CompareTag("Player"))
        {
            triggerActive = false;
            icon.SetActive(false);
            FindObjectOfType<LevelText>().HideText();

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
            }
        }
    }

    public void RunLevel(int num) {
        if (num == 1) {
            SceneManager.LoadScene("Level1");
            FindObjectOfType<GameManager>().CurrentActiveLevel(1);
            return;
        }

        else if (num == 2) {
            SceneManager.LoadScene("Level2");
            FindObjectOfType<GameManager>().CurrentActiveLevel(2);
            return;
        }

        else if (num == 3) {
            SceneManager.LoadScene("Level3");
            FindObjectOfType<GameManager>().CurrentActiveLevel(3);
            return;
        }
    }

    public void Interact()
    {
        //For controller input
        if (triggerActive) {
            FindObjectOfType<AudioManager>().Play("Object");
            RunLevel(levelNum);

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
                FindObjectOfType<Tutorial3>().HideInventoryControls();
            }
        }
    }
}
