using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouché
 * Last Update: 04/11/2024
 * Notes:       This script contains the logic for player health, etc. 
 */

public class PlayerController : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject shootPoint;
    public float bulletDelay = 2;

    private MoveController moveController;
    private bool hasShots = false;

    // when shoot, check MoveController.facingLeft. If true, instantiate bullet with facingLeft = true, else false. 

    private void Start()
    {
        moveController = gameObject.GetComponent<MoveController>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !hasShots)
        {
            StartCoroutine("shootDelay");
            if (moveController.facingLeft)
            {
              GameObject weakBullet = Instantiate(normalBullet, shootPoint.transform.position + new Vector3(-2.394f,0,0), Quaternion.identity);
              weakBullet.GetComponent<NormalBullet>().goingLeft = moveController.facingLeft;
                
            }
            else
            {
                GameObject weakBullet = Instantiate(normalBullet, shootPoint.transform.position, Quaternion.identity);
                weakBullet.GetComponent<NormalBullet>().goingLeft = moveController.facingLeft;
            }
        }
    }

    private IEnumerator shootDelay()
    {
        hasShots = true;
        yield return new WaitForSeconds(bulletDelay);
        hasShots = false;
    }
    

   
}
