using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    private Vector3 objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Debug.Log("Object Position - X Pos: " + transform.position.x + " // Y Pos: " + transform.position.y + " // Z Pos: " + transform.position.z);
    }

    public void ObjectStartPosition() {
        Debug.Log("Reset object");
        this.transform.position = objectPosition;
        this.transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().Sleep();
    }

}
