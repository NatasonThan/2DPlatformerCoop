using UnityEngine;

public class EnemyMushroom : Enemy
{
    private BoxCollider2D cd;

    protected override void Awake()
    {
        base.Awake();

        cd = GetComponent<BoxCollider2D>();
    }
    protected override void Update()
    {
        base.Update();

        anim.SetFloat("xVelocity", rb.linearVelocityX);

        if (isDead)
            return;

        HandleMovement();
        HandleCollision();
        
        if (isGrounded)
            HandleTrunAround();
    }

    private void HandleTrunAround()
    {
        if (!isGroundInfrontDeteced || isWallDetected)
        {
            Flip();
            idleTimer = idleDuration;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement() 
    {
        if (idleTimer > 0)
            return;

        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
    }

    public override void Die()
    {
        base.Die();

        cd.enabled = false;
    }

    /*public void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponent<PlayerManager>();

        if (player != null)
        {
            Destroy(gameObject, 1f);
        }
    }*/
}
