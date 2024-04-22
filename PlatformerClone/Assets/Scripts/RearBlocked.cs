using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Fouché, Els
 * Last Update: 04/22/2024
 * Notes:       This code, currently, links up with MoveBlocked.cs
 *              in order to determine if the player is able to move
 *              to the right. This script is attached to a set of 
 *              colliders on the left side of the player character. 
 */

public class RearBlocked : MonoBehaviour
{
    private bool isRearBlocked = false;

    /// <summary>
    /// If the player touches a wall (e.g. any object that's on the
    /// platform layer) then their movement to the left is blocked.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        isRearBlocked = true;
    }

    /// <summary>
    /// When the player moves away from a wall their movement to the
    /// left is no longer blocked. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        isRearBlocked = false;
    }

    /// <summary>
    /// Accessed by MoveBlocked.cs to determine if player's 
    /// leftward movement is blocked or not. 
    /// </summary>
    /// <returns></returns>
    public bool IsRearBlocked() {
        return isRearBlocked;
    }

    /// <summary>
    /// Accessed by Moveblocked to manually set player's blocked
    /// state in certain situations.
    /// </summary>
    /// <param name="rearBlocked"></param>
    public void SetRearBlocked(bool rearBlocked)
    {
        isRearBlocked = rearBlocked;
    }
}
