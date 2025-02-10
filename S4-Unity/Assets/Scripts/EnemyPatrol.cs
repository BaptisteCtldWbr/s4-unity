using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 3;
    public BoxCollider2D bc;
    public LayerMask listObstacleLayers;
    public float groundCheckRadius = 0.15f;
    public bool isFacingRight = true;
    public float DistanceDetection = 0.1f;
    
    public void FixedUpdate(){
        rb.linearVelocity = new Vector2(                    //Mouvement de l'enemy
            moveSpeed * transform.right.normalized.x,
            rb.linearVelocity.y
        );

        if(rb.linearVelocityY != 0){
            return;
        }

        if(HasNotTouchedGround()){
            Flip();
        }
    }

    void Flip(){
        if(
            (transform.right.normalized.x < 0 && !isFacingRight) ||
            (transform.right.normalized.x > 0 && isFacingRight)){
                transform.Rotate(0, 180, 0);
                isFacingRight = !isFacingRight;
        }
    }

    bool HasNotTouchedGround(){                             //Si il n'a pas d'obstacle sous lui
        Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
            bc.bounds.min.y
        );
        return !Physics2D.OverlapCircle(
            detectionPosition,
            groundCheckRadius,
            listObstacleLayers
        );
    }
