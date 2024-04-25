using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]

public class InventoryModel : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;
    private List<InventoryItem> operationItems;
    
    [SerializeField]
    private OpenAltar altar;
    
    [SerializeField]
    private List<Sprite> sprites;

    [field: SerializeField]
    public int Size{get;private set;} = 10;

    public event Action<Dictionary<int, InventoryItem>,List<InventoryItem>> OnInventoryUpdate;
    public event Action<int> OnPointerLeftClick;

    public void Initialize(){
        inventoryItems = new List<InventoryItem>();
        operationItems = new List<InventoryItem>(2);
        for(int i = 0; i<Size;i++){
                inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
        operationItems.Add(InventoryItem.GetEmptyItem());
        operationItems.Add(InventoryItem.GetEmptyItem());
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

    public void fill(int index){
        for(int i = 0;i<operationItems.Count;i++){
            if(operationItems[i].IsEmpty){
                operationItems[i] = inventoryItems[index];
                Debug.Log("fill ok");
                inventoryItems[index] = InventoryItem.GetEmptyItem();
                break;
            }
        }
        InformAboutChange();
    }

    public void getBack(int index){
        if(inventoryItems[index].IsEmpty){
            foreach(InventoryItem item in operationItems){
                if(item.IsEmpty == false){
                    inventoryItems[index] = item;
                    operationItems[index] = InventoryItem.GetEmptyItem();
                    break;
                }
            }
        }
        InformAboutChange();
    }

    public void LeftClick(int index){
        if(inventoryItems[index].IsEmpty){
            getBack(index);
            Debug.Log("leftClick");
        }
        else{
            fill(index);
            Debug.Log("leftClick else");
        }

        if (operationItems.Count >= 2 && operationItems[0].IsEmpty == false && operationItems[1].IsEmpty == false)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                Sprite valid_sprite = null;
                foreach (Sprite sprite in sprites)
                {
                    if ("number_" + Calcul() == sprite.name)
                    {
                        valid_sprite = sprite;
                        break;
                    }

                    foreach (InventoryItem item2 in inventoryItems)
                    {
                        if (item2.IsEmpty)
                        {
                            item2.item.sprite = valid_sprite;
                            break;
                        }
                    }
                }
            }
        }

        InformAboutChange();
    }

    public int Calcul(){
            string number1 = operationItems[0].item.sprite.name;
            string number2 = operationItems[1].item.sprite.name;
            int nb1 = Int32.Parse(number1.Substring(number1.Length -1,1));
            int nb2 = Int32.Parse(number2.Substring(number2.Length -1,1));
            int result = 0;
            switch(altar.getSprite().name){
                case "symbol_addition":
                    result = nb1 + nb2;
                    break;
                case "symbol_division":
                    if(nb1%nb2 == 0)
                    result = nb1/nb2;
                    else result =  -1;
                    break;
                case "symbol_substraction":
                    if(nb1-nb2 <0) result = -1;
                    else result =  nb1-nb2;
                    break;
                case "letter_x":
                    result = nb1*nb2;
                    break;
            }
            InformAboutChange();
            if(result < 10) return(result);
            else return(9);
    
    }

    public void InformAboutChange(){
        OnInventoryUpdate?.Invoke(GetCurrentInventoryState(),operationItems);
    }
    
    public void Clear(){
        for(int i = 0; i<inventoryItems.Count;i++){
            inventoryItems[i] = InventoryItem.GetEmptyItem();
        }
        InformAboutChange();
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