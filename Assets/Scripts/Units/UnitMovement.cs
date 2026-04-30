using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    private Vector2 targetPosition;
    private bool isMoving;

    private GameObject targetEnemy;
    private float lastAttackTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            HandleEnemyChaseAndAttack();
        }
        else if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, targetPosition) < 0.05f)
        {
            isMoving = false;
        }
    }

    void HandleEnemyChaseAndAttack()
    {
        if (targetEnemy == null) return;

        float dist = Vector2.Distance(rb.position, targetEnemy.transform.position);

        if (dist > attackRange)
        {
            Vector2 newPos = Vector2.MoveTowards(
                rb.position,
                targetEnemy.transform.position,
                moveSpeed * Time.deltaTime
            );

            rb.MovePosition(newPos);
        }
        else
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Health enemyHealth = targetEnemy.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    Debug.Log("Attacking enemy! HP: " + enemyHealth.hp);
                }

                lastAttackTime = Time.time;
            }
        }
    }

    public void MoveTo(Vector2 pos)
    {
        targetPosition = pos;
        isMoving = true;
        targetEnemy = null;
    }

    public void Attack(GameObject enemy)
    {
        targetEnemy = enemy;
        isMoving = false;
    }
}