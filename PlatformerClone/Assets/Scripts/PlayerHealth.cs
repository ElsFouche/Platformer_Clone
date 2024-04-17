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
    public int enemyNormalDamage = 15;
    public int enemyStrongDamage = 35;
    public int iSeconds = 5;            // Invulnerability time
    public float blinkSpeed = 0.5f;
    private bool isImmune = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();
            
            if (tags.tagType == TagManager.Tags.Enemies && isImmune == false)
            {
                switch(tags.enemyType) 
                {
                    case TagManager.Enemies.Normal:
                        PlayerDamaged(enemyNormalDamage);
                        break;
                    case TagManager.Enemies.Strong:
                        PlayerDamaged(enemyStrongDamage);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void PlayerDamaged(int damage)
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
        }
    }

    private IEnumerator Invulnerability() {
        isImmune = true;
        yield return new WaitForSeconds(iSeconds);
        isImmune = false;
    }

    private IEnumerator DamagedBlinking()
    {
        DamageBlink();
        for (int i = 0; i < (iSeconds/blinkSpeed - 1); i++) 
        {
            yield return new WaitForSeconds(blinkSpeed);
            DamageBlink();
        }
    }

    private void DamageBlink()
    {
        foreach(Transform child in gameObject.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = !child.GetComponent<MeshRenderer>().enabled;
        }
    }

    void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            Debug.Log(child.transform.name);
        }
    }
}