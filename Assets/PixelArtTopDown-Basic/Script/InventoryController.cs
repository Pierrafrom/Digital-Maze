using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventoryUI;

    public int inventorySize = 15;

    private void Start(){
        inventoryUI.InitializeInventory(inventorySize);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.isActiveAndEnabled == false){
                inventoryUI.Show();
            }
            else{
                inventoryUI.Hide();
            }
        }
    }
}
