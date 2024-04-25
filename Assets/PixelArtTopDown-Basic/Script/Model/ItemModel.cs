using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class ItemModel : ScriptableObject
{
    [field:SerializeField]
    public Sprite sprite{get;set;}

    public ItemModel(){
        this.sprite = null;
    }

}
