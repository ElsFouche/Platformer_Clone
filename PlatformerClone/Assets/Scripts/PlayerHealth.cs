using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author:      Els Fouche'
 * Last Update: 04/11/2024
 * Notes:       This script contains the logic for player health, etc. 
 */

public class PlayerHealth : MonoBehaviour
{
    public int health = 99;
    public int maxHealth = 99;
    public int healthUp = 25;
    public int enemyNormalDamage = 15;
    public int enemyStrongDamage = 35;
    public int iSeconds = 5;            // Invulnerability time
    public float blinkSpeed = 0.5f;
    public bool isImmune = false;

    /// <summary>
    /// This function is called when the player is damaged
    /// and accepts an int for the damage received. 
    /// It decrements the player health if they're not
    /// currently invulnerable and starts their invulnerability
    /// time and visual effect. If the player has no health left,
    /// the gameover scene is called and the player is disabled. 
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerDamaged(int damage)
    {
        if (!isImmune) 
        {
            health -= damage;
            StartCoroutine("Invulnerability");
            StartCoroutine("DamagedBlinking");
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(5);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void HealthUp()
    {
        if (health > maxHealth - healthUp)
        {
            health += maxHealth - health;
        } else
        {
            health += healthUp; 
        }
    }

    /// <summary>
    /// This function sets the flag for player invulnerability after
    /// being damaged and makes them vulnerable again after iSeconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Invulnerability() {
        isImmune = true;
        yield return new WaitForSeconds(iSeconds);
        isImmune = false;
    }

    /// <summary>
    /// This function handles the timing for the player blinking.
    /// The number of times the player should blink is dependent on
    /// the length of their invulnerability and the blink speed. 
    /// The final step of the function is to guarantee the player is
    /// visible. 
    /// </summary>
    /// <returns></returns>
    public IEnumerator DamagedBlinking()
    {
        DamageBlink();
        for (int i = 0; i < (iSeconds/blinkSpeed - 1); i++) 
        {
            yield return new WaitForSeconds(blinkSpeed);
            DamageBlink();
        }

        foreach(Transform child in gameObject.transform)
        {
            if (!child.GetComponent<MeshRenderer>().enabled)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    /// <summary>
    /// This function causes the player to blink when damaged.
    /// The blink is accomplished by disabling the mesh renderer for
    /// all of the children of the player body which does not contain mesh data itself.
    /// </summary>
    public void DamageBlink()
    {
        foreach(Transform child in gameObject.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = !child.GetComponent<MeshRenderer>().enabled;
        }
    }
}