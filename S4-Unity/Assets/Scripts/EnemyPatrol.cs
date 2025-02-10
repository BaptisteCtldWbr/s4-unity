using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 3;
    public BoxCollider2D bc;
    public LayerMask listObstacleLayers;
    public float groundCheckRadius = 0.15f;
    public bool isFacingRight = true;
    public float DistanceDetection = 0.7f;
    
    public void FixedUpdate(){
        rb.linearVelocity = new Vector2(                    //Mouvement de l'enemy
            moveSpeed * transform.right.normalized.x,
            rb.linearVelocity.y
        );

        if(rb.linearVelocityY != 0){
            return;
        }

        if(HasNotTouchedGround() || asCollisionWithObstacles()){
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

    bool asCollisionWithObstacles(){
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3(
                DistanceDetection * transform.right.normalized.x,
                0,
                0
            ), listObstacleLayers
        );
        return hit.transform != null;
    }

    public void OnDrawGizmos(){                             //Dessine le groundCheckStatus
        if(bc != null){
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                bc.bounds.center,
                bc.bounds.center + new Vector3(
                    DistanceDetection * transform.right.normalized.x,
                    0,
                    0
                )
            );
            Vector2 detectionPosition = new Vector2(
                bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
                bc.bounds.min.y
            );
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                detectionPosition,
                groundCheckRadius
            );
        }
    }
}
