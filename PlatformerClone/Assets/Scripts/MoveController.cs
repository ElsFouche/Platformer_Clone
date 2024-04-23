using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:      Els Fouche'
 * Last Update: 04/22/2024
 * Notes:       This script handles the player character's
 *              movement abilities. 
 */

public class MoveController : MonoBehaviour
{
    public int speed;
    public float jumpForce = 10.0f;
    public float jumpPowerup = 3.0f;
    public float dragForce = 5.0f;
    public float jumpDelayTime = 0.4f;
    public float minJumpTime = 0.2f;
    public bool facingLeft = false;
    public Collider forwardCollider;

    private Rigidbody rb;
    private JumpController jumpController;
    private GameObject playerBody;
    private MoveBlocked moveBlocked;
    private PlayerController playerController;
    private bool playerJumped = false;
    private bool minJumpReached = false;

    private enum Heading
    {
        GoingLeft,
        GoingRight
    }

    /// <summary>
    /// This code assigns the components necessary for movement to variables.
    /// </summary>
    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        jumpController = gameObject.GetComponentInChildren<JumpController>();
        
        playerBody = GameObject.Find("Player_Mesh");
        moveBlocked = gameObject.GetComponent<MoveBlocked>();

        playerController = gameObject.GetComponent<PlayerController>();
    }

    /// <summary>
    /// Movement code. Checks for key presses to determine player movement.
    /// Utilizes slerp to rotate player body to face movement direction. 
    /// Several checks are included to fix incorrect player-blocked reporting.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (jumpController.isGrounded && !playerJumped)
            {
                StartCoroutine("JumpDelay");
                PlayerJump();
                StartCoroutine("MinJumpTime");
            }
            else if (!playerJumped)
            {
                jumpController.CheckIfGrounded();
                if (jumpController.isGrounded)
                {
                    StartCoroutine("JumpDelay");
                    PlayerJump();
                    StartCoroutine("MinJumpTime");
                }
            }
        } 
        
        if (!Input.GetKey(KeyCode.W) && minJumpReached && !jumpController.isGrounded)
        {
            PlayerJumpDrag();
        }

        if (Input.GetKey(KeyCode.A) && !moveBlocked.IsRearBlocked())
        {
            facingLeft = true;
            PlayerMove(Heading.GoingLeft);
            playerBody.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), 1.0f);
        } 
        
        if (Input.GetKeyUp(KeyCode.A) && moveBlocked.IsRearBlocked())
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.left, out hit, 2.0f))
            {
                moveBlocked.SetRearBlocked(false);
            }
        }

        if (Input.GetKey(KeyCode.D) && !moveBlocked.IsFrontBlocked())
        {
            facingLeft = false;
            PlayerMove(Heading.GoingRight);
            playerBody.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f), 1.0f);
        } else if (Input.GetKeyUp(KeyCode.D) && moveBlocked.IsFrontBlocked())
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.right, out hit, 2.0f))
            {
                moveBlocked.SetFrontBlocked(false);
            }
        }
    }

    /// <summary>
    /// Handles lateral player movement.
    /// This code should be refactored to be physics based to conform to 
    /// the rest of the player's movement patterns. 
    /// </summary>
    /// <param name="direction"></param>
    private void PlayerMove(Heading direction)
    {
        if (direction == Heading.GoingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (direction == Heading.GoingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Adds force to the player to initiate a jump. If the player has the
    /// jump powerup unlocked, additional force is added to the jump. 
    /// </summary>
    private void PlayerJump()
    {
        if (!playerController.jumpPowerup)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        } else
        {
            rb.AddForce(Vector3.up * (jumpForce + jumpPowerup), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// This code adds downforce to the character if the player has released the
    /// jump button and a minimum amount of time has passed. 
    /// </summary>
    private void PlayerJumpDrag() 
    {
        rb.AddForce(Vector3.down * dragForce, ForceMode.Impulse);
    }

    /// <summary>
    /// This code determines whether enough time has elapsed for
    /// the player to be able to jump again. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator JumpDelay() {
        playerJumped = true;
        yield return new WaitForSeconds(jumpDelayTime);
        playerJumped = false;
    }

    /// <summary>
    /// This code handles the minimum jump time for the player.
    /// Currently this code only changes the minimum jump height
    /// if the value for minJumpTime is greater than coyotetime. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator MinJumpTime() 
    {
        minJumpReached = false;
        yield return new WaitForSeconds(minJumpTime);
        minJumpReached = true;
    }
}
