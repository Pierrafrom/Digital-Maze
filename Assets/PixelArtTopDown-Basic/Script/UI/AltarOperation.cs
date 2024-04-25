using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AltarOperation : MonoBehaviour
{
    [SerializeField] private Item item1;
    [SerializeField] private Item operation;
    [SerializeField] private Item item2;
    [SerializeField] private Inventory altarContent;
    [SerializeField] private List<Sprite> sprites;

    public Item getOperationItem()
    {
        return this.operation;
    }

    public void SetOperationSprite(Sprite sprite)
    {
        operation.SetData(sprite);
    }

    public void setUpItem1(Sprite item)
    {
        item1.SetData(item);
    }
    
    public void setUpItem2(Sprite item)
    {
        item2.SetData(item);
    }
    
    public Sprite getOperationSprite()
    {
        return this.operation.GetSprite();
    }

    public Item getItem1()
    {
        return this.item1;
    }

    public Sprite getItem1Sprite()
    {
        return this.item1.GetSprite();
    }

    public Item getItem2()
    {
        return this.item2;
    }

    public Sprite getItem2Sprite()
    {
        return this.item2.GetSprite();
    }
}
    
