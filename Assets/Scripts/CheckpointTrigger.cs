using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    // public Transform checkpoint;
    public Vector3 checkpointPosition;
    // private bool checkpointInitiated = false;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkpointPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    public void OnTriggerEnter (Collider checkpoint) {
        if(checkpoint.CompareTag("Player") && FindObjectOfType<Player>().PlayerCheckpointPosition() != this.checkpointPosition) {
            FindObjectOfType<AudioManager>().Play("Checkpoint");
            FindObjectOfType<CheckpointText>().ShowText();
            Debug.Log("Queue checkpoint!");
            // player.transform.position = checkpoint.position;
            
            FindObjectOfType<Player>().CheckpointPositionChange(checkpointPosition);
            // this.checkpointInitiated = true;
            // FindObjectOfType<Camera>().ChangeStartPosition(checkpointPosition);
        }
    }

    public void OnTriggerExit () {

    }
}
