using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTutorial : MonoBehaviour
{

    [SerializeField] private bool triggerActive = false;
 
        public void OnTriggerEnter(Collider npc)
        {
            if (npc.CompareTag("Player"))
            {
                triggerActive = true;
                Debug.Log("Press F to interact with the character");
            }
        }
 
        public void OnTriggerExit(Collider npc)
        {
            if (npc.CompareTag("Player"))
            {
                triggerActive = false;
            }
        }
 
        private void Update()
        {
            if (triggerActive && Input.GetKey(KeyCode.F))
            {
                Talk();
            }
        }
 
        public void Talk()
        {
            Debug.Log("Yes they are close");
        }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
