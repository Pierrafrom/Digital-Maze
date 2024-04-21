using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAltar : MonoBehaviour
{
    [SerializeField]
    private GameObject square;

    public Sprite getSprite(){
        SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
        return sr.sprite;
    }

}
