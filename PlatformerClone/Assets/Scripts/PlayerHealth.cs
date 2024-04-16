using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/11/2024
 * Notes:       This script contains the logic for player health, etc. 
 */

public class PlayerHealth : MonoBehaviour
{
    public int health = 99;
    public int enemyNormalDamage = 15;
    public int enemyStrongDamage = 35;
    public int iSeconds = 5;            // Invulnerability time

    private bool isImmune = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();
            
            if (isImmune == false) {
                if (tags.tagType == TagManager.Tags.Enemies)
                {
                    switch(tags.enemyType) 
                    {
                        case TagManager.Enemies.Normal:
                            health -= 15;
                            StartCoroutine("Invulnerability");
                            // TODO: Send new player health value to UI
                            break;
                        case TagManager.Enemies.Strong:
                            health -= 35;
                            StartCoroutine("Invulnerability");
                            // TODO: Send new player health value to UI
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private IEnumerator Invulnerability() {
        isImmune = true;
        yield return new WaitForSeconds(iSeconds);
        isImmune = false;
    }
}