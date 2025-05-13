using Unity.Netcode;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    [SerializeField] protected GameObject damageTrigger;
    [Space]
    [SerializeField] protected float moveSpeed = 2;
    [SerializeField] protected float idleDuration = 1.5f;
    protected float idleTimer;
    [Header("Death detils")]
    [SerializeField] private float deathImpactSpeed = 5;
    [SerializeField] private float deathRotationSpeed = 150;
    private int deathRotationDirection = 1;
    protected bool isDead;

    [Header("Basic collision")]
    [SerializeField] protected float groundCheckDistance = 1.1f;
    [SerializeField] protected float wallCheckDistance = .7f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;

    protected bool isGrounded;
    protected bool isWallDetected;
    protected bool isGroundInfrontDeteced;

    protected int facingDir = -1;
    protected bool facingRight = false;

    protected virtual void Awake() 
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() 
    {
        idleTimer -= Time.deltaTime;

        if (isDead) 
            HandleDeathRotation();
    }

    public virtual void Die()
    {
        if (IsServer)
        {
            DieClientRpc();
            Destroy(gameObject, 3f);
        }
    }

    [ClientRpc]
    private void DieClientRpc()
    {
        if (isDead) return;

        damageTrigger.SetActive(false);
        anim.SetTrigger("hit");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, deathImpactSpeed);
        isDead = true;

        AudioManager.instance.PlaySFX(9);

        if (Random.Range(0, 100) < 50)
            deathRotationDirection--;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestDieServerRpc()
    {
        Die();
    }

    private void HandleDeathRotation() 
    {
        transform.Rotate(0, 0, (deathRotationSpeed * deathRotationDirection) * Time.deltaTime);
    }

    protected virtual void HandleFlip(float xValue)
    {
        if (xValue < 0 && facingRight || xValue > 0 && !facingRight)
        {
            Flip();
        }
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isGroundInfrontDeteced = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDir), transform.position.y));
    }
}
