using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public float explosionForce = 3.0f;
    public float explosionRadius = 1.0f;
    public float despawnTime = 3.0f;
    public float offsetPos = -0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            TagManager tags = other.gameObject.GetComponent<TagManager>();

            if (tags.bulletType == TagManager.Bullet.SuperMissile)
            {
                foreach(BoxCollider collider in gameObject.GetComponentsInChildren<BoxCollider>())
                {
                    collider.enabled = false;
                }
                BlownUp(other.gameObject.transform.position);
            }
        }
    }

    private void BlownUp(Vector3 impactPoint)
    {
        StartCoroutine("DespawnTime");
        foreach (Transform child in gameObject.transform) 
        {
            if (child.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(explosionForce, impactPoint, explosionRadius, offsetPos);
            }
        }
    }

    private IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
