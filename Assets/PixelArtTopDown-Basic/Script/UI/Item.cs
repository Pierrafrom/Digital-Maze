using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IEndDragHandler,IDropHandler,IDragHandler
{
    [SerializeField]
    private Image itemImage;

    public event Action<Item> OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
    private bool empty = true;

    public void Awake()
    {
        ResetData();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void SetData(Sprite sprite)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        empty = false;
    }

    public void OnDrag(PointerEventData eventData){

    }

    public void OnDrop(PointerEventData eventData){
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData){
        if (empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData){
        OnItemEndDrag?.Invoke(this);    
    }

    public void OnPointerClick(PointerEventData eventData){
        PointerEventData pointerData = (PointerEventData)eventData;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
    }
}
