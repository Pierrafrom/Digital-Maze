using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventoryUI;

    private int[] inventorySize = new int[2];

    private void Start() {
        inventorySize[0] = (int)trunc(inventoryUI.getSizeX() / 180);
        inventorySize[1] = (int)trunc(inventoryUI.getSizeY() / 180);
        inventoryUI.InitializeInventory(inventorySize[0]* inventorySize[1]);
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

