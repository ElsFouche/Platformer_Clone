using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Symon Belcher
 * 4/18/2024
 * health pickup script
 */


public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
