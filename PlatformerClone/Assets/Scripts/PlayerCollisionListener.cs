using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Fouché, Els
 * Last Update: 04/22/2024
 * Notes:       This script handles various collision detection
 *              events for the player. It handles events where
 *              the player is touched by an enemy and events 
 *              when the player has touched a pickup. 
 */

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
    /// This is the event handler for when the player collides with other objects. 
    /// If the player touches an enemy they take damage. 
    /// If the player touches a powerup they permanently acquire that powerup.
    /// If the player touches a health pickup their health increases by a
    /// set amount or to their max health, whichever is lower. 
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
                    case TagManager.Pickups.HealthMax:
                        playerHealth.maxHealth += 100;
                        playerHealth.health += (playerHealth.maxHealth - playerHealth.health);
                        Destroy(other.gameObject);
                        break;
                    case TagManager.Pickups.Jump:
                        playerController.jumpPowerup = true;
                        Destroy(other.gameObject);
                        break;
                    case TagManager.Pickups.StrongBullet:
                        playerController.shotPowerup = true;
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
                        Destroy(other.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
