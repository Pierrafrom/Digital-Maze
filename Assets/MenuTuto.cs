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
        public GameObject infosTutosProps;


        private bool isMonsterDead = true;
        private bool isFirstMove = true;
        private bool isFirstProps = true;

        public void Resume()
        {
            menuTuto.SetActive(false);
            infosTuto.SetActive(false);
            infosTutosProps.SetActive(false);
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

        public void OnFirstProps()
        {
            if (isFirstProps && SceneManager.GetActiveScene().name == "tuto")
            {
                infosTutosProps.SetActive(true);
                Time.timeScale = 0;
                isFirstProps = false;
            }
        }
    }
}

