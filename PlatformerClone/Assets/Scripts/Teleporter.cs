using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameController gameController;
    public GameObject teleportDestination;
    public int levelIndex = -1;

    private Vector3 endPos;

    private void Start()
    {
        endPos = teleportDestination.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.GetComponent<TagManager>();

            if (tags.tagType == TagManager.Tags.Player)
            {
                Debug.Log(tags.tagType);
                gameController.player = other.gameObject;
                gameController.TeleportPlayer(endPos, levelIndex);
            }
        }
    }
}
