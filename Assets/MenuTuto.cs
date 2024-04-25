using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTuto : MonoBehaviour
{

public GameObject menuTuto;
public GameObject monster;

   private bool isMonsterDead = true;
    void Update()
    {
        
    }

    public void Resume()
    {
        menuTuto.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("test");
    }
}
