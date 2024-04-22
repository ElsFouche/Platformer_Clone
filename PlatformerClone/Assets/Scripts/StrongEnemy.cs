using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/22/2024
 * Notes:       This script handles the strong enemy's
 *              movement patterns. 
 */

public class StrongEnemy : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject playerTarget;
    public float findPlayerDelay = 0.5f;
    public bool rightBlocked = false;
    public bool leftBlocked = false;

    private Vector3 playerPos;
    private MoveBlocked moveBlocked;

    /// <summary>
    /// Searches for the player upon level start. 
    /// The player can not be assigned in the inspector 
    /// due to the 'live' player not necessarily being
    /// created in the same scene as the enemy. 
    /// </summary>
    private void Start()
    {
        playerTarget = GameObject.Find("player");
    }

    /// <summary>
    /// Begins tracking the player's position every few moments. 
    /// It handles objects blocking its movement in the same way
    /// as the player character. 
    /// </summary>
    void Awake()
    {
        InvokeRepeating("TrackPlayerPos", 0.0f, findPlayerDelay);
        moveBlocked = GetComponent<MoveBlocked>();
    }

    /// <summary>
    /// Chases the player based on the player's position relative to itself
    /// but only if it hasn't been blocked by a wall (e.g. any object on the
    /// "Platform" layer). 
    /// </summary>
    void Update()
    {
        if (!moveBlocked.IsFrontBlocked() && transform.position.x < playerPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        } else if (!moveBlocked.IsRearBlocked() && transform.position.x > playerPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
        } 
    }

    /// <summary>
    /// Updates the location the enemy would like to move towards. Called every
    /// findPlayerDelay seconds. 
    /// </summary>
    private void TrackPlayerPos()
    {
        playerPos = playerTarget.transform.position;
        playerPos.y = transform.position.y;
        playerPos.z = 0.0f;
    }
}
