using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "PF Player" && _animator.GetBool(open) && SceneManager.GetActiveScene().name == "tuto"){
            
            StartCoroutine(LoadLevel1AfterDelay(0.2f));
        }

        if (other.name == "PF Player" && _animator.GetBool(open) && SceneManager.GetActiveScene().name == "Lv1")
        {
            StartCoroutine(LoadLevelMenuAfterDelay(0.2f));
        }
    }
    
    IEnumerator LoadLevel1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Lvl1");
    }
    
    IEnumerator LoadLevelMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
    
}
