using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    [SerializeField] private int itemNumber; // Item ID

    public GameObject Object; //Exact item GameObject
    public GameObject Icon; //Exact item Icon
    public GameObject Player; //Player in level

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
    public void OnTriggerEnter(Collider item)
    {
        if (item.CompareTag("Player"))
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
    public void OnTriggerExit(Collider item)
    {
        if (item.CompareTag("Player"))
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
        if (triggerActive && Input.GetKeyDown(KeyCode.J))
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
            Object.SetActive(false);
            Player.GetComponent<Player>().CollectInventory(itemNumber);
        }
    }
}
