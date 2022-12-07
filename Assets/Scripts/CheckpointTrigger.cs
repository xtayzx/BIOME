using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public Vector3 checkpointPosition;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkpointPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    public void OnTriggerEnter (Collider checkpoint) {
        if(checkpoint.CompareTag("Player") && FindObjectOfType<Player>().PlayerCheckpointPosition() != this.checkpointPosition) {
            FindObjectOfType<AudioManager>().Play("Checkpoint");
            FindObjectOfType<CheckpointText>().ShowText();
            FindObjectOfType<Player>().CheckpointPositionChange(checkpointPosition);
        }
    }

    public void OnTriggerExit () {
    }
}
