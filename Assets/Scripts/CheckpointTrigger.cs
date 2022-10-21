using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    // public Transform checkpoint;
    public Vector3 checkpointPosition;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkpointPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    public void OnTriggerEnter (Collider checkpoint) {
        if(checkpoint.CompareTag("Player")) {
            FindObjectOfType<AudioManager>().Play("Checkpoint");
            Debug.Log("Entered checkpoint!");
            // player.transform.position = checkpoint.position;
            
            FindObjectOfType<Player>().CheckpointPositionChange(checkpointPosition);
            // FindObjectOfType<Camera>().ChangeStartPosition(checkpointPosition);
        }
    }

    public void OnTriggerExit () {

    }
}
