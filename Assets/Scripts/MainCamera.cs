using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 startPosition; // Starting position of the camera
    private Vector3 checkpointPosition; // New position of the camera once a checkpoint is reached

    // Each of the translation points of where to place the camera
    private float startX;
    private float startY;
    private float startZ;

    public Transform target;
    public Vector3 offset;

    void Start() {
        startPosition = this.transform.localPosition;
        Debug.Log("Camera Start Position: "+startPosition);

        startX = this.transform.localPosition.x;
        startY = this.transform.localPosition.y;
        startZ = this.transform.localPosition.z;
    }

    void Update() {
        transform.position = target.position +  offset;
    }
}
