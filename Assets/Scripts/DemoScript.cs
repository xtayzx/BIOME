using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id) {
       bool result = inventoryManager.AddItem(itemsToPickup[id]);
       if (result == true) {
            Debug.Log("Item added");
       }
       else {
            Debug.Log("Item not added");
       }
    }

    public void GetSelectedItem() {
     Item receivedItem = inventoryManager.GetSelectedItem();
     if(receivedItem != null) {
          Debug.Log("received item: "+receivedItem); 
     }
     
     else {
            Debug.Log("no item received!");
       }
    }

    public void UseItem() {
     Item receivedItem = inventoryManager.UseItem(true);
     if(receivedItem != null) {
          Debug.Log("item used: "+receivedItem); 
     }
     
     else {
            Debug.Log("no item used!");
       }
    }
}
