using UnityEngine;

public class TrapFalling : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D[] colliders;

    [SerializeField] private float speed = .75f;
    [SerializeField] private float travelDistance;
    private Vector3[] wayPoints;
    private int wayPointIndex;
    private bool canMove;

    [Header("Platform fall details")]
    [SerializeField] private float fallDelay = .5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<BoxCollider2D>();
    }
    private void Start()
    {
        SetupWayPoints();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() 
    {
        if (canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex]) < .1f)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Length)
                wayPointIndex = 0;
        }
    }
    private void SetupWayPoints() 
    {
        wayPoints = new Vector3[2];

        float yOffset = travelDistance / 2;

        wayPoints[0] = transform.position + new Vector3(0, yOffset, 0);
        wayPoints[1] = transform.position + new Vector3(0, -yOffset, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        if (player != null) 
        {
            Invoke(nameof(SwitchOffPlatform), fallDelay);
        }
    }
    private void SwitchOffPlatform() 
    {
        //anim.SetTrigger("deactivate");

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3.5f;
        rb.linearDamping = .5f;

        foreach (BoxCollider2D collider in colliders) 
        {
            collider.enabled = false;
        }
    }
}
