using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private RectTransform content;
    List<Item> itemList = new List<Item>();
    [SerializeField]
    private MouseFollower mouseFollower;

    private int draggedItemIndex = -1;

    public event Action<int> OnStartDragging;
    public event Action<int,int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
    }

    public void InitializeInventory(int size){
        for(int i = 0;i<size;i++){
            Item uiItem = Instantiate(item, Vector3.zero,Quaternion.identity);
            uiItem.transform.SetParent(content);
            itemList.Add(uiItem);
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void ResetAllItems(){
        foreach(var item in itemList){
            item.ResetData();
        }
    }

    public void UpdateData(int itemIndex,Sprite sprite){
        if(itemList.Count > itemIndex){
            itemList[itemIndex].SetData(sprite);
        }
    }

    private void HandleBeginDrag(Item item)
    {
        int index = itemList.IndexOf(item);
        if(index == -1)
            return;
        draggedItemIndex = index;
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite){
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite);
    }

    private void HandleSwap(Item item)
    {
        int index = itemList.IndexOf(item);
        if(index == -1){
            return;
        }
        OnSwapItems?.Invoke(draggedItemIndex,index);
    }

    public void ResetDraggedItem(){
        mouseFollower.Toggle(false);
        draggedItemIndex = -1;
    }

    private void HandleEndDrag(Item item)
    {
        ResetDraggedItem();
    }

    private void HandleShowItemActions(Item item)
    {

    }


    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public float getSizeX()
    {
        return this.content.rect.width;
    }

    public float getSizeY()
    {
        return this.content.rect.height;
    }
}
