using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Fouché, Els
 * Last Update: 04/22/2024
 * Notes:       This script is attached to the destructible wall
 *              object. When the wall is impacted by a super missile
 *              collisions are disabled to allow the player to move
 *              through it and the individual bricks of the wall
 *              have their gravity enabled, constraints disabled,
 *              and have an explosion force applied to them.
 */

public class DestructableWall : MonoBehaviour
{
    public float explosionForce = 3.0f;
    public float explosionRadius = 1.0f;
    public float despawnTime = 3.0f;
    public float offsetPos = -0.25f;

    /// <summary>
    /// Detects collisions from super missiles only.
    /// When detected, disables collisions and initiates
    /// the explosion function. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.bulletType == TagManager.Bullet.SuperMissile)
            {
                foreach(BoxCollider collider in gameObject.GetComponentsInChildren<BoxCollider>())
                {
                    collider.enabled = false;
                }
                BlownUp(other.gameObject.transform.position);
            }
        }
    }

    /// <summary>
    /// When called, begins a countdown until the wall is 
    /// eliminated from the game world. Then, for every
    /// object held in the wall, gravity is turned on, constraints
    /// are disabled, and an explosion force is applied to create
    /// a 'wall being blown up' effect. 
    /// </summary>
    /// <param name="impactPoint"></param>
    private void BlownUp(Vector3 impactPoint)
    {
        StartCoroutine("DespawnTime");
        foreach (Transform child in gameObject.transform) 
        {
            if (child.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(explosionForce, impactPoint, explosionRadius, offsetPos);
            }
        }
    }

    /// <summary>
    /// Countdown until the wall is eliminated from the game.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
