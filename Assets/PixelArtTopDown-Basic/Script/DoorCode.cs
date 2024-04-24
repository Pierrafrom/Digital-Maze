using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    [SerializeField]
    private GameObject square;

    private Animator _animator;
    private BoxCollider2D _collider;
    private static readonly int open = Animator.StringToHash("open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(open,false);

        _collider = gameObject.AddComponent<BoxCollider2D>();

    }

    public Sprite getNumberSprite(){
        SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
        return sr.sprite;
    }

    public void Open(){
        _animator.SetBool(open,true);
        
        _collider.enabled = false;
    }
}
