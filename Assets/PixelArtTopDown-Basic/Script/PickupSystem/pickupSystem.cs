using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tuto;

public class pickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventoryModel inventoryData;
    private bool alreadyPicked = false;
    
    public MenuTuto menuTuto;

    private void OnTriggerEnter2D(Collider2D collision){
        ItemPickup item = collision.GetComponent<ItemPickup>();
        if(item != null){
            int reminder = inventoryData.AddItem(item.InventoryItem);
            if(reminder == 0)
                item.DestroyItem();
        }

        if (!alreadyPicked)
        {
            alreadyPicked = true;
            menuTuto.OnFirstProps();
        }

    }
}
