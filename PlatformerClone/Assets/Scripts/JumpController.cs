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
    public float coyoteTime = 0.1f;

    public void CheckIfGrounded()
    {
        LayerMask layerMask = LayerMask.GetMask("Platform");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f, layerMask))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine("CoyoteTime");
    }

    private IEnumerator CoyoteTime() 
    {
        yield return new WaitForSeconds(coyoteTime);
        isGrounded = false;
    }
}