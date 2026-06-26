using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.3f;
    [SerializeField] private float waypointDistance = 1f;

    [Header("Patrulha")]
    public WaypointData[] waypoints;
    private int currentWaypointIndex = 0;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MovementAndJumpLogic();
    }

    private void MovementAndJumpLogic()
    {
        Vector3 target = new Vector3(waypoints[currentWaypointIndex].waypointPositionX, waypoints[currentWaypointIndex].waypointPositionY, waypoints[currentWaypointIndex].waypointPositionZ);

        float directionX = Mathf.Sign(target.x - transform.position.x);

        if (directionX > 0)
            spriteRenderer.flipX = true;
        else if (directionX < 0)
            spriteRenderer.flipX = false;

        rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);

        if (target.y > transform.position.y + 0.5f && canJump)
        {
            StartCoroutine(JumpRoutine());
        }

        float distanceToWaypoint = Vector2.Distance(transform.position, target);
        if (distanceToWaypoint < waypointDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private IEnumerator JumpRoutine()
    {
        canJump = false;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        yield return new WaitForSeconds(jumpCooldown);

        canJump = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeakPoint"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}