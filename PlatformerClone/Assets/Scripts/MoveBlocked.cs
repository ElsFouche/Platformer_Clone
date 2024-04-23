using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/22/2024
 * Notes:       This scripts stops entities from moving through 
 *              solid objects. Objects must be on the "Platform"
 *              layer in order to block entity movement. 
 */

public class MoveBlocked : MonoBehaviour
{
    private FrontBlocked frontBlocked;
    private RearBlocked rearBlocked;

    /// <summary>
    /// Assign frontBlocked & rearBlocked scripts
    /// based on scripts held in the relevant collision
    /// objects attached to the entity. 
    /// </summary>
    void Start() 
    {
        frontBlocked = GetComponentInChildren<FrontBlocked>();
        rearBlocked = GetComponentInChildren<RearBlocked>();
    }

    /// <summary>
    /// Accessor that communicates with FrontBlocked script
    /// to determine if the right side of the entity is blocked.
    /// </summary>
    /// <returns></returns>
    public bool IsFrontBlocked() 
    {
        return frontBlocked.IsFrontBlocked();
    }

    /// <summary>
    /// Accessor that communicates with Rearblocked script
    /// to determine if the left side of the entity is blocked.
    /// </summary>
    /// <returns></returns>
    public bool IsRearBlocked() 
    {
        return rearBlocked.IsRearBlocked();
    }

    /// <summary>
    /// Mutator that allows for the right side of the entity
    /// to be manually switched between blocked or not. 
    /// </summary>
    /// <param name="isFrontBlocked"></param>
    public void SetFrontBlocked(bool isFrontBlocked)
    {
        frontBlocked.SetFrontBlocked(isFrontBlocked);
    }

    /// <summary>
    /// Mutator that allows for the right side of the entity
    /// to be manually switched between blocked or not. 
    /// </summary>
    /// <param name="isRearBlocked"></param>
    public void SetRearBlocked(bool isRearBlocked)
    {
        rearBlocked.SetRearBlocked(isRearBlocked);
    }
}
