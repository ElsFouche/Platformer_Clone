using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Fouché, Els
 * Last Update: 04/20/2024
 * Notes:       This script teleports the player based on
 *              whether a GameObject transform has been set or
 *              if a desired scene destination has been set. 
 */

public class Teleporter : MonoBehaviour
{
    [Tooltip("Required: Set in prefab to use GameController prefab!")]
    public GameController gameController;
    [Tooltip("Optional. If blank, uses the level index to determine target location.")]
    public GameObject teleportDestination;
    [Tooltip("Optional. If -1, uses the manually set teleport destination.")]
    public int levelIndex = -1;

    /// <summary>
    /// Only teleports objects marked as the player based on the 
    /// entity's tagmanager data. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.GetComponent<TagManager>();

            if (tags.tagType == TagManager.Tags.Player)
            {
                // Debug.Log(tags.tagType);
                gameController.player = other.gameObject;
                StartCoroutine("WaitToReactivate"); 
                TeleportPlayerIf();
            }
        }
    }

    /// <summary>
    /// Teleports the player based on whether an in-level or 
    /// scene destination has been set. 
    /// </summary>
    private void TeleportPlayerIf()
    {
        if (teleportDestination != null)
        {
            gameController.TeleportPlayer(teleportDestination.transform.position);
        } else if (levelIndex != -1)
        {
            gameController.TeleportPlayer(levelIndex);
        }
    }

    /// <summary>
    /// Prevents double activations resulting in poor performance. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitToReactivate()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
}
