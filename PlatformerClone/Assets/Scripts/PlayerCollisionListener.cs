using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.pickupType == TagManager.Pickups.Jump)
            {
                playerController.jumpPowerup = true;
                Destroy(other.gameObject);
            }
        }
    }
}
