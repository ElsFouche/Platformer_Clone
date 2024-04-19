using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/11/2024
 * Notes:       This script handles various game events, 
 *              manages information to be sent to the UI,
 *              and various additional functions related to
 *              game function. 
 */

public class GameController : MonoBehaviour
{
    public GameObject player;
    private EndScene sceneTransition;

    public GameObject[] spawnPoints;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TeleportPlayer(Vector3 destination, int sceneIndex = -1)
    {
        player.transform.parent.transform.position = destination;
        Debug.Log(destination);
        Debug.Log("Player location: " + player.transform.position);

        if (sceneIndex != -1)
        {
            sceneTransition.SwitchScene(sceneIndex);
        }
    }
}
