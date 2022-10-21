using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    PlayerControls controls;
    public GameObject player;
    // public Camera mainCamera;

    // private float newX;
    // private float newY;
    // private float newZ;

    // private bool trigger;

    void Start () {
        // Set game frame rate - cause my fans are going crazy so I think this sets it up
        Application.targetFrameRate = 60;
        // trigger = false;
    }

    // void FixedUpdate() {
    //     if(FindObjectOfType<Player>().playerIsFalling() == false) {
    //         newX = mainCamera.transform.position.x;
    //         newY = mainCamera.transform.position.y;
    //         newZ = mainCamera.transform.position.z;
    //     }

    //     else if(FindObjectOfType<Player>().playerIsFalling() == true) {
    //         trigger = true;
    //     }

    //     if(trigger == true) {
    //         StopCamera();
    //         trigger = false;
    //         return;
    //     }
    // }

    // void StopCamera() {
    //     mainCamera.transform.position = new Vector3(newX, newY, newZ);
    //     // FindObjectOfType<AudioManager>().Play("Checkpoint");
    // }

    // public void ActivateControls(string controllerType) {
    //     controls = new PlayerControls();

    //     if(controllerType == "MenuControls") {
    //         controls.Menu.Select.performed += ctx => Select();
    //     }

    //     else if(controllerType == "PauseControls") {
    //         controls.Gameplay.Menu.performed += ctx => Pause();
    //     }

    //     else if(controllerType == "PlayControls") {
    //     }
    // }

    // void Select() {

    // }

    // void Pause() {

    // }
    
    // void Talk() {

    // }

    // void Conversation() {

    // }

    // void Jump() {

    // }

    // public void EnableMenuControls() {
    //     controls.Menu.Enable();
    // }

    // public void DisableMenuControls() {
    //     controls.Menu.Disable();
    // }

    public void CompleteLevel() {

        FreezeGame();
        FindObjectOfType<AudioManager>().Pause("MainSong");
        FindObjectOfType<AudioManager>().Play("End");

        // GameIsPaused = true;
        Debug.Log("LEVEL WON");
        completeLevelUI.SetActive(true);
    }

    public void FreezeGame() {
        //freezes the game
        Time.timeScale = 0f;
    }

    // public void UnfreezeGame() {
    //     //unfreezes the game
    //     Time.timeScale = 1f;
    // }
    
    public void SelectFirstButton(GameObject firstButton) {

        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstButton);

    }
}
