using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    private Vector3 objectPosition;

    void Start()
    {
        objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Resetting the object when the game is reset to the previous checkpoint
    public void ObjectStartPosition() {
        // Debug.Log("Reset object");
        this.transform.position = objectPosition;
        this.transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().Sleep(); //TODO *
    }

}
