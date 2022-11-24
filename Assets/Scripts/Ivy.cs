using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivy : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void OnTriggerEnter(Collider ivy)
    {
        if (ivy.CompareTag("Player"))
        {
            FindObjectOfType<PauseMenu>().FallResetGame();
        }
    }

    // public void OnCollisionEnter(Collision collision) {
    //     //WATER LAYER
    //     if (collision.gameObject.layer == 3) {
    //         if(inWater == true) {
    //         FindObjectOfType<AudioManager>().Play("Splash");
    //         Debug.Log("Entered Water");
    //         speed = waterSpeed;
    //         inWater = false;
    //         return;
    //         }
    //     }
    //     // isGrounded = true;
    // }

    // public void OnCollisionExit(Collision collision) {
    //     // isGrounded = false;

    //     // WATER LAYER
    //     if (collision.gameObject.layer != 3 && inWater == false) {
    //         FindObjectOfType<AudioManager>().Play("Splash");
    //         Debug.Log("Exited water");
    //         speed = landSpeed;
    //         inWater = true;
    //     }
    // }
}
