using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

/* Author:      Fouché, Els
 * Last Update: 04/22/2024
 * Notes:       This code handles enemy health values.
 *              Enemies blink briefly when being shot to 
 *              indicate they've been hit. When an enemy's
 *              health drops to 0 or below, it is destroyed.
 *              The script checks upon creation of the enemy
 *              to make sure the health has been initialized to
 *              a valid amount. 
 */
public class EnemyHealth : MonoBehaviour
{
    public int health = 0;
    public float damagedBlinkSpeed = 0.25f;
    public float damagedBlinkTime = 1.0f;

    /// <summary>
    /// Checks immediately to see if the enemy has a valid health valu.
    /// </summary>
    void Start()
    {
        if (health <= 0)
        {
            Debug.Log("Enemy health not set or is negative! Destroying.");
        }
    }

    /// <summary>
    /// If the enemy's health ever falls to 0 or below it is destroyed.
    /// </summary>
    void Update()
    {
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// If the enemy is hit by a bullet, any bullet, 
    /// they begin blinking to indicate they've been hit. 
    /// Enemy health values are manipulated directly by the
    /// impacting bullet. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>(); 

            if (tags.bulletType != TagManager.Bullet.None)
            {
                StartCoroutine("DamagedBlinking");
            }
        }
    }

    /// <summary>
    /// This code functions identically to the player's blink-on-damage
    /// function set. 
    /// </summary>
    /// <returns></returns>
    public IEnumerator DamagedBlinking()
    {
        DamageBlink();
        for (int i = 0; i < (damagedBlinkTime / damagedBlinkSpeed - 1); i++)
        {
            yield return new WaitForSeconds(damagedBlinkSpeed);
            DamageBlink();
        }

        if (this.GetComponent<MeshRenderer>() != null)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (Transform child in gameObject.transform)
        {
            if (!child.GetComponent<MeshRenderer>().enabled)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    /// <summary>
    /// This code functions identically to the player's blink-on-damage
    /// function set. 
    /// </summary>
    public void DamageBlink()
    {
        if (this.GetComponent<MeshRenderer>() != null)
        { 
            this.GetComponent<MeshRenderer>().enabled = !this.GetComponent<MeshRenderer>().enabled;
        }
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = !child.GetComponent<MeshRenderer>().enabled;
            }
        }
    }

    /// <summary>
    /// Destroys the enemy. This probably doesn't need to be
    /// its own function. 
    /// </summary>
    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
