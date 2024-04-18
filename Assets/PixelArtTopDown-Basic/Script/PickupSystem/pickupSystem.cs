using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventoryModel inventoryData;

    private void OnTriggerEnter2D(Collider2D collision){
        ItemPickup item = collision.GetComponent<ItemPickup>();
        if(item != null){
            int reminder = inventoryData.AddItem(item.InventoryItem);
            if(reminder == 0)
                item.DestroyItem();
        }
    }
}
