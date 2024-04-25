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
    private Inventory operationInventory;
    [SerializeField]
    private AltarOperation altarOperation;
    [SerializeField]
    private List<OpenAltar> altars;
    [SerializeField]
    private DoorCode door;
    private OpenAltar currentAltar;

    public List<InventoryItem> initialItems = new List<InventoryItem>();


    private void Start() {
        PrepareUI();
        PrepareInventoryData();
        
    }

    private void PrepareInventoryData(){
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdate += UpdateInventory;
        inventoryData.OnPointerLeftClick += LeftClick;
        foreach(InventoryItem item in initialItems){
            if(item.IsEmpty)continue;
            inventoryData.AddItem(item);
        }
        altarOperation.getOperationItem().SetData(altarOperation.getOperationSprite());
    }

    private void UpdateInventory(Dictionary<int,InventoryItem> inventoryState, List<InventoryItem> operationitems){
        inventoryUI.ResetAllItems();
        inventoryUI.updateItemList(inventoryState);
        if(altarInventory.isActiveAndEnabled == true){
            altarOperation.getOperationItem().SetData(altarOperation.getOperationSprite());
            if(!operationitems[0].IsEmpty){
                altarOperation.setUpItem1(operationitems[0].item.sprite);
            } 
            else {
                altarOperation.setUpItem1(null);
            }
            if(!operationitems[1].IsEmpty) {
                altarOperation.setUpItem2(operationitems[1].item.sprite);
            }
            else {
                altarOperation.setUpItem2(null);
            }
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
        this.altarContent.OnLeftClick += LeftClick;
        this.operationInventory.OnSwapItems += HandleSwapItems;
        this.operationInventory.OnStartDragging += HandleDragging;
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

    private void LeftClick(int index){
        inventoryData.LeftClick(index,currentAltar);
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

    private void OnTriggerEnter2D(Collider2D collision){
        foreach(OpenAltar altar in altars){
            if(collision.name == altar.name){
                currentAltar = altar;
                altarOperation.SetOperationSprite(altar.getSprite());
                if(altarInventory.isActiveAndEnabled == false){
                altarInventory.Show();
                altarContent.Show();
                operationInventory.Show();
                foreach(var item in inventoryData.GetCurrentInventoryState()){
                    altarContent.UpdateData(item.Key,item.Value.item.sprite);
                }
            }
            else{
            altarInventory.Hide();
            }
            }
        }
        if(collision.name == "PF Props Wooden Gate"){
            foreach(Item item in inventoryUI.getItemList()){
                if(item.GetSprite()==door.getNumberSprite()){
                    door.Open();
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision){
         foreach(OpenAltar altar in altars){
            if(collision.name == altar.name){
                altarInventory.Hide();
            }
         }
    }
    public void ClearInventory()
    {
        // Clear the inventory data
        inventoryData.Clear();

        // Update the UI
        if(inventoryUI.isActiveAndEnabled == true)
        {
            inventoryUI.ResetAllItems();
        }
        else if(altarInventory.isActiveAndEnabled == true)
        {
            altarContent.ResetAllItems();
            operationInventory.ResetAllItems();
        }
    }
    
}


