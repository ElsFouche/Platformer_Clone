using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public float explosionForce = 3.0f;
    public float explosionRadius = 1.0f;
    public float despawnTime = 3.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.bulletType == TagManager.Bullet.SuperMissile)
            {
                BlownUp(other.gameObject.transform.position);
            }
        }
    }

    private void BlownUp(Vector3 impactPoint)
    {
        StartCoroutine("DespawnTime");
        foreach (Transform child in gameObject.transform) 
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(explosionForce, impactPoint, explosionRadius, -0.25f);
        }
    }

    private IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
