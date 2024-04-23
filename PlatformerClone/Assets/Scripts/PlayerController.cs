using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouché & Symon Belcher
 * Last Update: 04/18/2024
 * Notes:       This script allows the player to shoot. 
 *              It also keeps track of which powerups the
 *              player has access to. 
 */

public class PlayerController : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject strongBullet;
    public GameObject superMissile;
    public GameObject shootPoint;
    public GameObject shootPointLeft;
    public float bulletDelay = 2;
    public int superMissiles = 0;
    public int superMissilePickupCount = 5;
    public int maxSuperMissiles = 50;


    private MoveController moveController;
    private bool hasShots = false;
    public bool jumpPowerup = false;
    public bool shotPowerup = false;
    public bool healthPowerup = false;

    /// <summary>
    /// The player remains persistant across levels.
    /// We access the MoveController attached to the player. 
    /// </summary>
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        moveController = gameObject.GetComponent<MoveController>();
    }

    /// <summary>
    /// Handles checking for player input. If the player is 
    /// attempting to shoot, fires the correct bullet based on
    /// which powerups the player has. 
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !hasShots)
        {
            StartCoroutine("shootDelay");
            if (shotPowerup)
            {
                ShootBullet(strongBullet);
            } else
            {
                ShootBullet(normalBullet);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && !hasShots)
        {
            StartCoroutine("shootDelay");
            if (superMissiles > 0)
            {
                superMissiles -= 1;
                ShootBullet(superMissile);
            }
        }
    }

    /// <summary>
    /// Prevents the player from shooting. The player may
    /// shoot every bulletDelay seconds. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator shootDelay()
    {
        hasShots = true;
        yield return new WaitForSeconds(bulletDelay);
        hasShots = false;
    }

    /// <summary>
    /// Creates a bullet when called that moves across the screen.
    /// The bullet is rotated to face the same direction as the player upon
    /// creation. 
    /// </summary>
    /// <param name="bullet"></param>
    private void ShootBullet(GameObject bullet)
    {
        if (moveController.facingLeft)
        {
            GameObject shotBullet = Instantiate(bullet, shootPointLeft.transform.position, Quaternion.identity);
            shotBullet.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), 1.0f);
        }
        else
        {
            GameObject shotBullet = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        }
    }
}
