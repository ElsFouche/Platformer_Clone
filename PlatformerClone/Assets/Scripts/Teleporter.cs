using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Tooltip("Required: Set in prefab to use GameController prefab!")]
    public GameController gameController;
    [Tooltip("Optional. If blank, uses the level index to determine target location.")]
    public GameObject teleportDestination;
    [Tooltip("Optional. If -1, uses the manually set teleport destination.")]
    public int levelIndex = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.GetComponent<TagManager>();

            if (tags.tagType == TagManager.Tags.Player)
            {
                Debug.Log(tags.tagType);
                gameController.player = other.gameObject;
                TeleportPlayerIf();
            }
        }
    }

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
}
