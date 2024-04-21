using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/*
 * Symon Belcher
 * 4/16/2024
 * end screen when player dies 
 */

    public class EndScene : MonoBehaviour
    {


        public void QuiteGame() // exits game
        {

            // print("quite game");
            Application.Quit();
        }

        public void SwitchScene(int buildIndex)// switches scene to platofmer game from end scene
        {
            SceneManager.LoadScene(buildIndex);
        }

    }

