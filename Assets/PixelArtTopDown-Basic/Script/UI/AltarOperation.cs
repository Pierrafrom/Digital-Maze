using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AltarOperation : MonoBehaviour
{
    [SerializeField]
    private Item item1;
    [SerializeField]
    private Item operation;
    [SerializeField]
    private Item item2;
    [SerializeField]
    private Inventory altarContent;
    [SerializeField]
    private List<Sprite> sprites;

    public void Fill(Item item){
        if(item1.IsEmpty()){
            item1.SetData(item.GetSprite());
            item.ResetData();
        }
        else if(item2.IsEmpty()){
            item2.SetData(item.GetSprite());
            item.ResetData();
        }
        Debug.Log("Fill");
    }

    public void getBack(Item item,int index){
        Debug.Log("get back");
        if(item1.GetSprite()!=null && item1.IsEmpty() == false){
            altarContent.UpdateData(index,item1.GetSprite());
            item1.ResetData();
        }
        else if(item2.GetSprite()!=null&& item2.IsEmpty() == false){
            altarContent.UpdateData(index,item2.GetSprite());
            item2.ResetData();
        }
    }

    public Item getOperationItem(){
        return this.operation;
    }

    public void SetOperationSprite(Sprite sprite){
        operation.SetData(sprite);
    }

    public Sprite getOperationSprite(){
        return this.operation.GetSprite();
    }

    public Item getItem1(){
        return this.item1;
    }

    public Sprite getItem1Sprite(){
        return this.item1.GetSprite();
    }
    public Item getItem2(){
        return this.item2;
    }

    public Sprite getItem2Sprite(){
        return this.item2.GetSprite();
    }

    private int Calcul(){
        string number1;
        string number2;
        int nb1;
        int nb2;
        number1 = item1.GetSprite().name;
        number2 = item2.GetSprite().name;
        nb1 = Int32.Parse(number1.Substring(number1.Length -1,1));
        nb2 = Int32.Parse(number2.Substring(number2.Length -1,1));
        int result = 0;
        switch(operation.GetSprite().name){
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
        if(result < 10) return(result);
        else return(9);
    }

    public void Update(){
        if(item1.IsEmpty() == false && item2.IsEmpty() == false && item1.GetSprite() != null && item2.GetSprite() != null){
            int result = Calcul();
            if(result == -1) return;
            Debug.Log(result);
            item1.ResetData();
            item2.ResetData();
            foreach(Sprite sprite in sprites){
                if("number_"+result == sprite.name){
                    for(int i =0;i<altarContent.getItemList().Count;i++){
                        if(altarContent.getItemList()[i].IsEmpty()){
                            altarContent.UpdateData(i,sprite);
                            break;
                        }
                    }
                    break;
                }
                Debug.Log(sprite.name);
            }
            
        }
    }
}
