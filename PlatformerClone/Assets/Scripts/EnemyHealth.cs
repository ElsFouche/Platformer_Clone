using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class EnemyHealth : MonoBehaviour
{
    public int health = 0;
    public float damagedBlinkSpeed = 0.25f;
    public float damagedBlinkTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (health <= 0)
        {
            Debug.Log("Enemy health not set or is negative! Destroying.");
        }
    }
    void Update()
    {
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

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

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
