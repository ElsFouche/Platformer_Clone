using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author:      Els Fouche'
 * Last Update: 04/22/2024
 * Notes:       This script handles loading scenes and
 *              interfaces with Teleporter.cs in order
 *              to move the player during scene changes
 *              and manual teleport situations. 
 */

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;

    /// <summary>
    /// This script holds reference to the locations the player
    /// will be teleported to at level start. We preserve the 
    /// game object carrying this script across loads so that
    /// updates to those spawn points, should any be needed, 
    /// are correctly tracked. Remember to update the live 
    /// game object with a reference to the non-prefab spawn
    /// points in such a situation!
    /// </summary>
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// When called with an int, teleports the player to the 
    /// corresponding level and spawn location. 
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void TeleportPlayer(int sceneIndex = -1)
    {

        if (sceneIndex != -1 && sceneIndex < spawnPoints.Length - 1)
        {
            // Debug.Log("Moving to level " + sceneIndex);
            // Debug.Log("Teleport destination: " + spawnPoints[sceneIndex].transform.position);
            player = GetTopParent(player);
            player.transform.position = spawnPoints[sceneIndex].transform.position;
            // Debug.Log("Player location after teleport: " + player.transform.position);
            SceneManager.LoadScene(sceneIndex);
        } else
        {
            Destroy(GameObject.Find("player"));
            SceneManager.LoadScene(sceneIndex);
        }
    }

    /// <summary>
    /// When called with an int, teleports the player to the 
    /// corresponding level and spawn location. 
    /// </summary>
    /// <param name="destination"></param>
    public void TeleportPlayer(Vector3 destination)
    {
        player = GetTopParent(player);
        player.transform.position = destination;
        Debug.Log("Teleport destination: " + destination);
        Debug.Log("Player location after teleport: " + player.transform.position);
    }

    /// <summary>
    /// When called, returns the game object at the top of the 
    /// hierarchy of the input object. 
    /// </summary>
    /// <param name="inObject"></param>
    /// <returns></returns>
    private GameObject GetTopParent(GameObject inObject)
    {
        while (inObject.transform.parent != null)
        {
            inObject = inObject.transform.parent.gameObject;
        }

        return inObject;
    }
}
