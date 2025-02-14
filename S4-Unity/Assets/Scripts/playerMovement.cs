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

    public VoidEventChannel onPlayerDeath;
    private void OnEnable()
    {
        onPlayerDeath.OnEventRaised += Die;
    }

    private void OnDisable()
    {
        onPlayerDeath.OnEventRaised -= Die;       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Die(){
        enabled = false;
    }

    // Update is called once per frame
    void Update()                                           //Ne pas faire de physique, problème de timing / vitesse, cette focntion est appelée selon le FPS
    {
        moveDirectionX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJump){
            Jump();                                         //Saute s'il a le droit
            currentNumberJumps++;
        }

        if(isGrounded && !Input.GetButton("Jump")){         //reset du nombre de jump s'il est au sol et ne saute pas
            currentNumberJumps = 0;
        }
        
        Flip();
    }

    void Flip(){
        if(                                                 //Fonction pour se retourner
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

    private void Jump(){                                    //Saut
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );
    }

    public bool IsGrounded(){                               //Vérifie s'il y a le sol dessous
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            listGroundLayers
        );
    }

    public void OnDrawGizmos(){                             //Dessine le groundCheckStatus
        if(groundCheck != null){
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}
