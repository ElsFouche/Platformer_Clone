using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int speed;
    public float jumpForce = 10.0f;
    public bool facingLeft = false;

    private Rigidbody rb;
    private JumpController jumpController;
    private GameObject playerBody;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpController.isGrounded)
            {
                PlayerJump();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            facingLeft = true;
            PlayerMove(Heading.GoingLeft);
            playerBody.transform.rotation = Quaternion.Slerp(Quaternion.identity, new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), 1.0f);
        }

        if (Input.GetKey(KeyCode.D))
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
}
