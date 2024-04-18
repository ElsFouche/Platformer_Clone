using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public bool goingLeft;

    public float speed = 15;
    public float despawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer(despawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (goingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.enemyType != TagManager.Enemies.None)
            {
                Destroy(other.gameObject);
            }
        }

        Destroy(gameObject);
    }
    private IEnumerator DespawnTimer(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
