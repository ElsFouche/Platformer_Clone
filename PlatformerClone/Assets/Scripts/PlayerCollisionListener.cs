using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerController = gameObject.transform.parent.GetComponent<PlayerController>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    /// <summary>
    /// This is the event handler for when the player collides with
    /// other objects. 
    /// If enemy, take damage. 
    /// If health pickup, add health.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.tagType == TagManager.Tags.Enemies && !playerHealth.isImmune)
            {
                switch (tags.enemyType)
                {
                    case TagManager.Enemies.Normal:
                        playerHealth.PlayerDamaged(playerHealth.enemyNormalDamage);
                        break;
                    case TagManager.Enemies.Strong:
                        playerHealth.PlayerDamaged(playerHealth.enemyStrongDamage);
                        break;
                    default:
                        break;
                }
            } else if (tags.tagType == TagManager.Tags.Pickup)
            {
                switch (tags.pickupType)
                {
                    case TagManager.Pickups.Health:
                        playerHealth.HealthUp();
                        Destroy(other.gameObject);
                        break;
                    case TagManager.Pickups.Jump:
                        playerController.jumpPowerup = true;
                        Destroy(other.gameObject);
                        break;
                    case TagManager.Pickups.SuperMissile:
                        if (playerController.superMissiles > playerController.maxSuperMissiles - playerController.superMissilePickupCount)
                        {
                            playerController.superMissiles += playerController.maxSuperMissiles - playerController.superMissiles;
                        } else
                        {
                            playerController.superMissiles += playerController.superMissilePickupCount;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
