using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public CreateItem thisItem;
    public CreateBag playerInventory;
    private void Awake() {
        //playerInventory = InventoryManager.instance.myBag;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem() {
        if (!playerInventory.items.Contains(thisItem)) {
            // playerInventory.itemList.Add(thisItem);
            // InventoryManager.CreateNewItem(thisItem);
            for (int i = 0; i < playerInventory.items.Count; i++) {
                if (playerInventory.items[i] == null) {
                    playerInventory.items[i] = thisItem;
                    break;
                }
            }
        }
        else {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
