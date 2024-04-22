using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject[] spawnPoints;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TeleportPlayer(int sceneIndex = -1)
    {

        if (sceneIndex != -1)
        {
        Debug.Log("Moving to level " + sceneIndex);
        Debug.Log("Teleport destination: " + spawnPoints[sceneIndex].transform.position);
            player = GetTopParent(player);
            player.transform.position = spawnPoints[sceneIndex].transform.position;
        Debug.Log("Player location after teleport: " + player.transform.position);
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void TeleportPlayer(Vector3 destination)
    {
        player = GetTopParent(player);
        player.transform.position = destination;
        Debug.Log("Teleport destination: " + destination);
        Debug.Log("Player location after teleport: " + player.transform.position);
    }

    private GameObject GetTopParent(GameObject inObject)
    {
        while (inObject.transform.parent != null)
        {
            inObject = inObject.transform.parent.gameObject;
        }

        return inObject;
    }
}
