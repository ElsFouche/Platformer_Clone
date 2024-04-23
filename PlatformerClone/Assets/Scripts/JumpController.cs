using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/22/2024
 * Notes:       This script manages the player's ability to jump.
 *              It includes code for coyote time and operates via 
 *              box collision events. Raycasts are used in certain
 *              situations to circumvent double trigger events. 
 */

public class JumpController : MonoBehaviour
{
    public bool isGrounded = true;
    public float coyoteTime = 0.1f;

    /// <summary>
    /// When called, checks if the player is on a platform. If so, 
    /// force-sets isGrounded to true. 
    /// </summary>
    public void CheckIfGrounded()
    {
        LayerMask layerMask = LayerMask.GetMask("Platform");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f, layerMask))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// When the jumpBox collider interacts with a collider on an object on the "Platform"
    /// layer, sets isGrounded to true. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    /// <summary>
    /// When the player leaves a platform, allows them a brief grace 
    /// period in which they can still jump. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine("CoyoteTime");
    }

    /// <summary>
    /// Handles the timing for the player being able to jump despite
    /// not being on a platform any longer. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoyoteTime() 
    {
        yield return new WaitForSeconds(coyoteTime);
        isGrounded = false;
    }
}