using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/*
 * Author:      Symon Belcher
 * Last Update: 4/22/2024
 * Notes:       end screen when player dies 
 */

    public class EndScene : MonoBehaviour
    {
    /// <summary>
    /// Switches scenes when Player dies, clicks restart, or play
    /// </summary>
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