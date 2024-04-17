using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int speed;
    public float jumpForce = 10.0f;
    public float dragForce = 5.0f;
    public float jumpDelayTime = 0.4f;
    public float minJumpTime = 0.2f;
    public bool facingLeft = false;
    public Collider forwardCollider;

    private Rigidbody rb;
    private JumpController jumpController;
    private GameObject playerBody;
    private MoveBlocked moveBlocked;
    private bool playerJumped = false;
    private bool minJumpReached = false;

    private enum Heading
    {
        GoingLeft,
        GoingRight
    }

    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        jumpController = gameObject.GetComponentInChildren<JumpController>();
        
        playerBody = GameObject.Find("Player_Mesh");
        moveBlocked = gameObject.GetComponent<MoveBlocked>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpController.isGrounded && !playerJumped)
            {
                StartCoroutine("JumpDelay");
                PlayerJump();
                StartCoroutine("MinJumpTime");
            }
        }

        if (!Input.GetKey(KeyCode.Space) && minJumpReached && !jumpController.isGrounded)
        {
            PlayerJumpDrag();
        }

        if (Input.GetKey(KeyCode.A) && !moveBlocked.IsRearBlocked())
        {
            facingLeft = true;
            PlayerMove(Heading.GoingLeft);
            playerBody.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), 1.0f);
        }

        if (Input.GetKey(KeyCode.D) && !moveBlocked.IsFrontBlocked())
        {
            facingLeft = false;
            PlayerMove(Heading.GoingRight);
            playerBody.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f), 1.0f);
        }
    }

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

    private void PlayerJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void PlayerJumpDrag() 
    {
        rb.AddForce(Vector3.down * dragForce, ForceMode.Impulse);
    }

    private IEnumerator JumpDelay() {
        playerJumped = true;
        yield return new WaitForSeconds(jumpDelayTime);
        playerJumped = false;
    }

    private IEnumerator MinJumpTime() 
    {
        minJumpReached = false;
        yield return new WaitForSeconds(minJumpTime);
        minJumpReached = true;
    }
}
