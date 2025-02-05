using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveDirectionX = 0f;
    public float moveSpeed = 10f;
    public float jumpForce = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
    }

    private void FixedUpdate(){
        rb.linearVelocity = new Vector2(
            moveDirectionX * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void Jump(){
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
    }
}
