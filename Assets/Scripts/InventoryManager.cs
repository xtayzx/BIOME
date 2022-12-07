using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    private int maxCount = 6;

    public int selectedSlot = 0; //IMPORTANT FOR ITEM SELECTION LATER

    PlayerControls controls;

    private void Start() {
        ChangeSelectedSlot(0);
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Awake() {
            controls = new PlayerControls();
            controls.Gameplay.InventoryIncrease.performed += ctx => IncreaseSlot();
            controls.Gameplay.InventoryDecrease.performed += ctx => DecreaseSlot();
        }

    private void Update() {
        if (Input.inputString != null) {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 7) {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    public void IncreaseSlot() {
        int value = selectedSlot;
        if (value <= 5) {
            value++;
        }

        if (value > 5) {
            value = 0;
        }

        ChangeSelectedSlot(value);
    }

    public void DecreaseSlot() {
        int value = selectedSlot;
        if (value >= 0) {
            value--;
        }

        if (value < 0) {
            value = 5;
        }

        ChangeSelectedSlot(value);
    }

    void ChangeSelectedSlot(int newValue) {
        if(selectedSlot >= 0) {
            inventorySlots[selectedSlot].Deselect();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
        FindObjectOfType<AudioManager>().Play("InventoryToggle");
    }

    public bool AddItem(Item item) {

        //check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
            itemInSlot.item == item &&
            itemInSlot.count < maxCount &&
            itemInSlot.item.stackable == true) {

                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    //Removing Ducks in Level 3
    public Item RemoveItem(Item item) {

        //check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
            itemInSlot.item == item &&
            itemInSlot.count < maxCount &&
            itemInSlot.item.stackable == true) {

                itemInSlot.count = 0;
                itemInSlot.RefreshCount();
                Destroy(itemInSlot.gameObject);
                return itemInSlot.item;
            }
        }

        return null;
    }

    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem() {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            return itemInSlot.item;
        }
        return null;
    }

    public Item UseItem(bool use) {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {

            Item item = itemInSlot.item;
            if (use == true && item.stackable == true) {
                itemInSlot.count--;
                if (itemInSlot.count <= 0) {
                    Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefreshCount();
                }
            }

            return itemInSlot.item;
        }
        return null;
    }
}
