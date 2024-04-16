using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/11/2024
 * Notes:       This script manages the player's ability to jump.
 *              It includes code for coyote time and operates via 
 *              box collision events. 
 */

public class JumpController : MonoBehaviour
{
    public bool isGrounded = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<TagManager>() == null)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<TagManager>() == null)
        {
            isGrounded = false;
        }
    }
}