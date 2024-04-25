using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tuto
{
    public class MenuTuto : MonoBehaviour
    {

        public GameObject menuTuto;
        public GameObject monster;
        public GameObject infosTuto;


        private bool isMonsterDead = true;
        private bool isFirstMove = true;

        public void Resume()
        {
            menuTuto.SetActive(false);
            infosTuto.SetActive(false);
            Time.timeScale = 1;
        }
    
        public void OnCharacterMove()
        {
            if (isFirstMove && SceneManager.GetActiveScene().name == "tuto")
            {
                infosTuto.SetActive(true);
                Time.timeScale = 0;
                isFirstMove = false;
            }
        }
    }
}

