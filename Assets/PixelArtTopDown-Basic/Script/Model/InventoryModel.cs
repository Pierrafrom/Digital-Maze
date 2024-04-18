using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]

public class InventoryModel : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size{get;private set;} = 10;

    public event Action<Dictionary<int,InventoryItem>> OnInventoryUpdate;

    public void Initialize(){
        inventoryItems = new List<InventoryItem>();
        for(int i = 0; i<Size;i++){
                inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    public int AddItem(ItemModel item){
        for(int i = 0; i<inventoryItems.Count;i++){
            if(inventoryItems[i].IsEmpty){
                inventoryItems[i] = new InventoryItem{
                    item = item
                };
                return 0;
            }
        }
        return 0;
    }

    public void AddItem(InventoryItem item){
        AddItem(item.item);
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState(){
        Dictionary<int,InventoryItem> returnValue = new Dictionary<int,InventoryItem>();

        for(int i = 0; i<inventoryItems.Count;i++){
            if(inventoryItems[i].IsEmpty) continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int index){
        return inventoryItems[index];
    }

    public void SwapItems(int index1,int index2){
        InventoryItem tmp = inventoryItems[index1];
        inventoryItems[index1] = inventoryItems[index2];
        inventoryItems[index2] = tmp;
        InformAboutChange();
    }

    public void InformAboutChange(){
        OnInventoryUpdate?.Invoke(GetCurrentInventoryState());
    }
}

[Serializable]
public struct InventoryItem{
    public ItemModel item;
    public bool IsEmpty => item == null;

    public InventoryItem addItem(){
        return new InventoryItem{
            item = this.item
        };
    }

    public static InventoryItem GetEmptyItem() => new InventoryItem {
        item = null
    };
}