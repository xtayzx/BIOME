using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Apple : MonoBehaviour
{
    // THIS SCRIPT IS NOT CURRENTLY ACTIVE
    [SerializeField] private bool triggerActive = false;

    public GameObject AppleObject;
    public GameObject Icon;
    public GameObject Player;
    GameManager gameManager;

    PlayerControls controls;

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

    //When the player enters the boundaries, then allow them to interact with the object
    public void OnTriggerEnter(Collider apple)
    {
        if (apple.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Object");
            triggerActive = true;
            Icon.SetActive(true);

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().ShowControls();
            }
        }
    }

    //When the player exits the boundaries, turn off UI visuals
    public void OnTriggerExit(Collider apple)
    {
        if (apple.CompareTag("Player"))
        {
            triggerActive = false;
            Icon.SetActive(false);

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
            }
        }
    }

    private void Update()
    {
        //Keyboard Action
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        //For controller input
        if (triggerActive) {
            FindObjectOfType<AudioManager>().Play("Interact");

            if(FindObjectOfType<LevelManager>().Tutorial() == true) {
                FindObjectOfType<Tutorial>().HideControls();
            }

            //Remove object and put in inventory
            AppleObject.SetActive(false);
            Player.GetComponent<Player>().CollectInventory(2);
        }
    }
}
