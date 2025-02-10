using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveDirectionX = 0f;
    public float moveSpeed = 10f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded = false;
    public LayerMask listGroundLayers;
    public int maxAllowedJump = 3;
    public int currentNumberJumps = 0;

    public bool isFacingRight = true;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()                                           //Ne pas faire de physique, problème de timing / vitesse, cette focntion est appelée selon le FPS
    {
        moveDirectionX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJump){
            /* if(!isGrounded){
                currentNumberJumps = 1;
            } */
            Jump();
            currentNumberJumps++;
        }

        if(isGrounded && !Input.GetButton("Jump")){
            currentNumberJumps = 0;
        }
        
        Flip();
    }

    void Flip(){
        if(
            (moveDirectionX < 0 && !isFacingRight) ||
            (moveDirectionX > 0 && isFacingRight)){
                transform.Rotate(0, 180, 0);
                isFacingRight = !isFacingRight;
        }
    }

    private void FixedUpdate(){                             //Fais la physique ici, pour ne pas avoir de problèmes lié au temps.
        rb.linearVelocity = new Vector2(
            moveDirectionX * moveSpeed,
            rb.linearVelocity.y
        );
        isGrounded = IsGrounded();
    }

    private void Jump(){
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
    }

    public bool IsGrounded(){
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            listGroundLayers
        );
    }

    public void OnDrawGizmos(){
        if(groundCheck != null){
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}
