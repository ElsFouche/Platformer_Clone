using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int speed;
    public float jumpForce = 10.0f;
    public float gravityForce = 9.81f;

    private Rigidbody rb;
    private JumpController jumpController;

    private enum Heading
    {
        GoingLeft,
        GoingRight
    }

    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        jumpController = gameObject.GetComponentInChildren<JumpController>();
    }

    void Update()
    {
        // transform.position += Vector3.down * gravityForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpController.isGrounded)
            {
                PlayerJump();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            PlayerMove(Heading.GoingLeft);
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerMove(Heading.GoingRight);
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
