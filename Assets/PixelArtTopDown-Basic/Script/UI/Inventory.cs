using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private RectTransform content;
    List<Item> itemList = new List<Item>();
    [SerializeField]
    private MouseFollower mouseFollower;
    public Sprite sprite;

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

    private void HandleBeginDrag(Item obj)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite);
    }

    private void HandleSwap(Item obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleEndDrag(Item obj)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleShowItemActions(Item obj)
    {
        item.SetData(sprite);
    }


    public void Show(){
        gameObject.SetActive(true);
        itemList[0].SetData(sprite);
    }

    public void Hide(){
        gameObject.SetActive(false);
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
