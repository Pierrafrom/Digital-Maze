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
    [SerializeField]
    private Inventory altarInventory;
    [SerializeField]
    private Inventory altarContent;
    [SerializeField]
    private Inventory altarOperation;

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
        inventoryData.OnInventoryUpdate += UpdateInventory;
        foreach(InventoryItem item in initialItems){
            if(item.IsEmpty)continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventory(Dictionary<int,InventoryItem> inventoryState){
        if(inventoryUI.isActiveAndEnabled == true){
                inventoryUI.ResetAllItems();
            foreach(var item in inventoryState){
                inventoryUI.UpdateData(item.Key,item.Value.item.sprite);
            }
        }
        else if(altarInventory.isActiveAndEnabled == true){
                altarContent.ResetAllItems();
            foreach(var item in inventoryState){
                altarContent.UpdateData(item.Key,item.Value.item.sprite);
            }
        }
    }


    private void PrepareUI(){
        inventoryUI.InitializeInventory(inventoryData.Size);
        altarContent.InitializeInventory(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.altarInventory.OnSwapItems += HandleSwapItems;
        this.altarInventory.OnStartDragging += HandleDragging;
        this.altarContent.OnSwapItems += HandleSwapItems;
        this.altarContent.OnStartDragging += HandleDragging;
        this.altarOperation.OnSwapItems += HandleSwapItems;
        this.altarOperation.OnStartDragging += HandleDragging;
    }

    private void HandleDragging(int index){
        InventoryItem inventoryItem = inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty) return;
        if(inventoryUI.isActiveAndEnabled == true){
            inventoryUI.CreateDraggedItem(inventoryItem.item.sprite);
        }
        else if(altarInventory.isActiveAndEnabled == true){
            altarInventory.CreateDraggedItem(inventoryItem.item.sprite);
        }

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
        else if(Input.GetKeyDown(KeyCode.J)){
            if(altarInventory.isActiveAndEnabled == false){
                altarInventory.Show();
                altarContent.Show();
                altarOperation.Show();
                foreach(var item in inventoryData.GetCurrentInventoryState()){
                    altarContent.UpdateData(item.Key,item.Value.item.sprite);
                }
            }
            else{
            altarInventory.Hide();
            }
        }
        
    }
}


