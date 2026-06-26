using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 6f;

    [Header("Input")]
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private KeyCode jumpKey;

    [Header("Vida e Spawn")]
    public int lifesCount = 3;
    public Transform spawnPoint;

    private Rigidbody2D rb;
    private float inputHorizontal;
    private bool jumpRequested;
    private SpriteRenderer spriteRenderer;

    [Header("Gerenciadores")]
    public EnemyGenerator enemyGenerator;
    public PontuationManager pontuationManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);

        if (inputHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(jumpKey))
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        Move();

        if (jumpRequested)
        {
            Jump();
        }
    }

    private void Move()
    {
        float targetSpeed = inputHorizontal * moveSpeed;
        float rate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float newVelocityX = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, rate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        jumpRequested = false;
    }

    public void Die()
    {
        lifesCount--;

        if (lifesCount <= 0)
        {
            Debug.Log("Game Over!");
            return;
        }

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void KillEnemy(GameObject weakPoint)
    {
        if (enemyGenerator != null) enemyGenerator.EnemyDeathManager();
        if (pontuationManager != null) pontuationManager.points += 100;

        GameObject enemyRoot = weakPoint.transform.root.gameObject;

        Destroy(enemyRoot);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeakPoint"))
        {
            KillEnemy(collision.gameObject);
        }
    }
}