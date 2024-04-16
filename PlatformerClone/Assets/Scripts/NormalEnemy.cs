using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Symon Belcher
 * 4/11/2024
 * normal enemy controls, normal enemy will be killed after one hit
 */

public class NormalEnemy : MonoBehaviour
{
    public float speed;

    //stores game objects positioned to enemys lfet and right
    public GameObject leftPoint;
    public GameObject rightPoint;
    public bool movingLeft;

    // positions of left and right boundary objects
    private Vector3 leftPos;
    private Vector3 rightPos;
    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagManager>() != null)
        {
            Debug.Log(other.gameObject);
            TagManager otherTag = other.gameObject.GetComponent<TagManager>();

            // Checks if enemy collides with a bullet, destroys enemy after one hit
            if (otherTag.tagType == TagManager.Tags.Bullet)
            {
                Destroy(gameObject);
            }

        }
    }

    private void Move()

    {
        Vector3 moveDir;

        if (movingLeft)
        {
            moveDir = Vector3.left;

            //check if enemy has eached left boundry
            if (transform.position.x < leftPos.x)
            {
                //flips enemys direction
                movingLeft = false;
            }

        }
        else//going right
        {
            moveDir = Vector3.right;


            if (transform.position.x > rightPos.x)
            {
                //flips enemys direction
                movingLeft = true;

            }
        }
        transform.Translate(moveDir * speed * Time.deltaTime);
    }
}