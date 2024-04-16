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
    // Start is called before the first frame update
    public void InitializeInventory(int size){
        for(int i = 0;i<size;i++){
            Item uiItem = Instantiate(item, Vector3.zero,Quaternion.identity);
            uiItem.transform.SetParent(content);
            itemList.Add(uiItem);
        }
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
