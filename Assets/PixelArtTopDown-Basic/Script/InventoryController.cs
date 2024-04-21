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
        altarOperation.getOperationItem().SetData(altarOperation.getOperationSprite());
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
                operationInventory.ResetAllItems();
            foreach(var item in inventoryState){
                altarContent.UpdateData(item.Key,item.Value.item.sprite);
            }
            altarOperation.getOperationItem().SetData(altarOperation.getOperationSprite());
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
        if(altarInventory.isActiveAndEnabled == true){
            Item item = altarContent.getItemList()[index];
            if(item.IsEmpty()) altarOperation.getBack(item,index);
            else altarOperation.Fill(item);
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
    }

    private void OnTriggerEnter2D(Collider2D collision){
        foreach(OpenAltar altar in altars){
            Debug.Log(collision.name);
            Debug.Log(altar.name);
            if(collision.name == altar.name){
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

    }

    private void OnTriggerExit2D(Collider2D collision){
         foreach(OpenAltar altar in altars){
            Debug.Log(collision.name);
            Debug.Log(altar.name);
            if(collision.name == altar.name){
                altarInventory.Hide();
            }
         }
    }
}


