using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Symon Belcher
 * 4/19/2024
 * Script for the normal bullet, controlls its movments and destroys enemies
 */
public class NormalBullet : MonoBehaviour
{
    public float speed = 15;
    public int damage = 1;
    public float despawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer(despawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.enemyType != TagManager.Enemies.None)
            {
                other.gameObject.GetComponent<EnemyHealth>().health -= damage;
            } 
            
            if (tags.tagType != TagManager.Tags.Player)
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator DespawnTimer(float time) // despawns the bullet
    {

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
