using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    public DrawingManager drawingManager;
    public float movementForce = 1;
    public float jumpForce = 1;
    public float strafingModifier = 0.6f;

    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private Transform groundCheckMiddle = null;

    [NonSerialized]
    public bool isGrounded;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        isGrounded = groundCheck();

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (drawingManager.gameMode == GameMode.MOVING && horizontalInput !=0)
            Walk(horizontalInput);

        if (Input.GetAxis("Jump") > 0 && drawingManager.gameMode == GameMode.MOVING && isGrounded)
        {
            Jump();
        }

    }
    void Walk(float horizontalInput)
    {
        float forceX = horizontalInput * movementForce;

        if (!isGrounded)
            forceX *= strafingModifier;

        playerRigidbody.AddForce(new Vector2(forceX, 0));

        //if (horizontalInput > 0)
        //    playerRigidbody.transform.localScale = new Vector3(1, transform.localScale.y);
        //else
        //    playerRigidbody.transform.localScale = new Vector3(-1, transform.localScale.y);

       
    }

    void Jump()
    {
        float forceY = jumpForce;
        playerRigidbody.AddForce(new Vector2(0, forceY));
    }

    bool groundCheck()
    {
        return
            Physics2D.Linecast(transform.position, groundCheckMiddle.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
