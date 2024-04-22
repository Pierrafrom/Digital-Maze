using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    [SerializeField]
    private GameObject square;

    private Animator _animator;
    private static readonly int open = Animator.StringToHash("open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public Sprite getNumberSprite(){
        SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
        return sr.sprite;
    }

    public void Open(){
        _animator.SetBool(open,true);
    }
}
