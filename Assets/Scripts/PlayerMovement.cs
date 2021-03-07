using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    public DrawingManager drawingManager;
    public Animator anim;
    public float movementForce = 1;
    public float jumpForce = 1;
    public float climbForce = 1;
    public float strafingModifier = 0.6f;
    public float ladderHorizontalModifier = 0.2f;
    public float jumpCoolDown = 0.1f;
    public Box box;

    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private Transform groundCheckMiddle = null;
    [SerializeField]
    private Transform groundCheckLeft = null;
    [SerializeField]
    private Transform groundCheckRight = null;

    private bool isGrounded;
    private bool IsOnLadder = false;
    private float direction = 1;
    private float xScale = 0.8f;
    private bool lockJump = false;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        xScale = transform.localScale.x;
    }


    void FixedUpdate()
    {
        isGrounded = groundCheck();
        anim.SetBool("Grounded", isGrounded);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (drawingManager.gameMode == GameMode.MOVING)
            Walk(horizontalInput);

        if (Input.GetAxis("Jump") > 0 && drawingManager.gameMode == GameMode.MOVING && isGrounded && !lockJump)
        {
            Jump();           
        }

        if (IsOnLadder)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * ladderHorizontalModifier, 0);
            float verticallInput = Input.GetAxisRaw("Vertical");
            if (verticallInput != 0)
            {
                playerRigidbody.AddForce(new Vector2(0, verticallInput * climbForce));
            }
        }

        anim.SetBool("Moving", playerRigidbody.velocity != Vector2.zero);
        anim.SetBool("MoveKeyPressed", horizontalInput != 0);
        anim.SetBool("Climbing", IsOnLadder);

    }
    void Walk(float horizontalInput)
    {
        float forceX = horizontalInput * movementForce;

        playerRigidbody.velocity = new Vector2(forceX, playerRigidbody.velocity.y);

        direction = horizontalInput >= 0 ? 1 : -1;
        direction *= xScale;
        
        if (horizontalInput != 0 && direction != playerRigidbody.transform.localScale.x)
        {
            playerRigidbody.transform.localScale = new Vector3(direction, transform.localScale.y);
            box.FlipX();
        }

        if (!isGrounded)
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * strafingModifier, playerRigidbody.velocity.y);

    }

    void Jump()
    {
        lockJump = true;
        float forceY = jumpForce;
        playerRigidbody.AddForce(new Vector2(0, forceY));
        anim.SetTrigger("Jump");
        StartCoroutine(UnlockJump());
    }

    bool groundCheck()
    {
        return
            Physics2D.Linecast(transform.position, groundCheckMiddle.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void SetLadder(bool Ladder)
    {
        IsOnLadder = Ladder;
    }

    IEnumerator UnlockJump()
    {
        yield return new WaitForSeconds(jumpCoolDown);
        lockJump = false;
    }

}
