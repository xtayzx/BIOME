using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 checkpointPosition;

    // private float newX;
    // private float newY;
    // private float newZ;
    // private bool trigger;

    private float startX;
    private float startY;
    private float startZ;


    public Transform target;
    public Vector3 offset;

    void Start() {
        startPosition = this.transform.localPosition;
        Debug.Log("Camera Start Position: "+startPosition);
        // trigger = false;

        startX = this.transform.localPosition.x;
        startY = this.transform.localPosition.y;
        startZ = this.transform.localPosition.z;
    }

    void Update() {
        transform.position = target.position +  offset;
    }

    // public void ResetCamera() {
    //     // this.transform.localPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);
    //     // trigger = false;
    //     this.transform.localPosition = new Vector3(startX, startY, startZ);

    //     Debug.Log("Camera successfully reset: " + this.transform.localPosition);
    // }

    // void Update() {
    //     if(FindObjectOfType<Player>().playerIsFalling() == false) {
    //         newX = this.transform.position.x;
    //         newY = this.transform.position.y;
    //         newZ = this.transform.position.z;
    //     }

    //     else if(FindObjectOfType<Player>().playerIsFalling() == true) {
    //         trigger = true;
    //     }

    //     if(trigger == true) {
    //         StopCamera();
    //     }
    // }

    // void StopCamera() {
    //     this.transform.position = new Vector3(newX, newY, newZ);
    //     // FindObjectOfType<AudioManager>().Play("Checkpoint");
    // }

    // public void ChangeStartPosition(Vector3 newPosition) {
    //     checkpointPosition = newPosition;

    //     Debug.Log("Camera position has been changed: "+ startPosition);
    // }
}
