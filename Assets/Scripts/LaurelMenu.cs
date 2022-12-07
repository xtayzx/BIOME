using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LaurelMenu : MonoBehaviour
{
    public static bool LaurelLoaded = false;
    public GameObject laurelMenuUI, inventoryControls, inventory, checkpoint;
    PlayerControls controls;
    private GameManager gameManager;

    private int gameState;
    public GameObject player;

    public TextMeshProUGUI taskText;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Objective.performed += ctx => Objective();

        gameState = FindObjectOfType<GameManager>().GetActiveLevel();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Start() {
        //If game is in the level hub
        if(gameState == 0) {
            taskText.text = "Adventure around. What do the animals of the land say?";
        }

        //If at level 1
        if(gameState == 1) {
            taskText.text = "Is there any food that you can find for the bears?";
        }

        //If at level 2
        if(gameState == 2) {
            taskText.text = "Can you find Rabbit's friend?";
        }

        //If at level 3
        if(gameState == 3) {
            taskText.text = "Where are all of Mama Duck's ducklings?";
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && player.GetComponent<Player>().startConvoActive() == true) {
            if (LaurelLoaded) {
                Resume();
            }
            else {
                Objective();
            }
        }
    }

    public void Resume() {
        laurelMenuUI.SetActive(false);
        inventory.SetActive(true);
        checkpoint.SetActive(true);

        taskText.overrideColorTags = true;
        taskText.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,0);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(true);
        }

        Time.timeScale = 1f;
        LaurelLoaded = false;
        FindObjectOfType<GameManager>().SetPaused(false);
    }

    public void Objective() {
        if (LaurelLoaded) {
            Resume();
            return;
        }
        
        laurelMenuUI.SetActive(true);
        inventory.SetActive(false);
        checkpoint.SetActive(false);

        if(FindObjectOfType<LevelManager>().Tutorial() == true) {
            inventoryControls.SetActive(false);
        }
        
        //Freezes the game
        Time.timeScale = 0f;
        LaurelLoaded = true;
        FindObjectOfType<GameManager>().SetPaused(true);

        taskText.overrideColorTags = true;
        taskText.GetComponent<TextMeshProUGUI>().color = new Color32 (255,255,255,255);
    }
}