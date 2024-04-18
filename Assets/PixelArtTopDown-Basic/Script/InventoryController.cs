using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventoryUI;
    [SerializeField]
    private InventoryModel inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();


    private void Start() {
        /*inventorySize[0] = (int)trunc(inventoryUI.getSizeX() / 180);
        inventorySize[1] = (int)trunc(inventoryUI.getSizeY() / 180);
        inventoryUI.InitializeInventory(inventorySize[0]* inventorySize[1]);*/
        PrepareUI();
        PrepareInventoryData();
        
    }

    private void PrepareInventoryData(){
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdate += UpdateInventoryUI;
        foreach(InventoryItem item in initialItems){
            if(item.IsEmpty)continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int,InventoryItem> inventoryState){
        inventoryUI.ResetAllItems();
        foreach(var item in inventoryState){
            inventoryUI.UpdateData(item.Key,item.Value.item.sprite);
        }
    }


    private void PrepareUI(){
        inventoryUI.InitializeInventory(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
    }

    private void HandleDragging(int index){
        InventoryItem inventoryItem = inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty) return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.sprite);
    }

    private void HandleSwapItems(int index1, int index2){
        inventoryData.SwapItems(index1,index2);
    }

    public void Update(){
        
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.isActiveAndEnabled == false){
                inventoryUI.Show();
                foreach(var item in inventoryData.GetCurrentInventoryState()){
                    inventoryUI.UpdateData(item.Key,item.Value.item.sprite);
                }
            }
            else{
                inventoryUI.Hide();
            }
        }
    }
}

