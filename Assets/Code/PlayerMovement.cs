using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    Rigidbody2D rb;
    bool isGrounded;
    float groundCheckRadius = 0.4f;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //left right
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(moveInput!= 0 )
        {
            if(moveInput < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if (moveInput > 0)
                GetComponent<SpriteRenderer>().flipX = false;

            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("jump");
        }
    }
}
