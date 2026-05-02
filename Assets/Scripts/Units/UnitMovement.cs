using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float attackRange = 3f;
    public float damage = 10f;
    public float attackCooldown = 1f;
    public float detectionRange = 4f;

    private bool attackMove = false;
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
        if (attackMove && targetEnemy == null)
        {
            GameObject enemy = FindClosestEnemy();

            if (enemy != null)
            {
                float dist = Vector2.Distance(rb.position, enemy.transform.position);

                if (dist <= detectionRange)
                {
                    targetEnemy = enemy;
                    isMoving = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            HandleEnemyChaseAndAttack();
        }
        else if (isMoving)
        {
            MoveToTarget();
        }
        if (!isMoving && targetEnemy == null)
{
    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
}
    }

    void MoveToTarget()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, targetPosition) < 0.3f)
{
    isMoving = false;
    attackMove = false;
    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
}
    }

    void HandleEnemyChaseAndAttack()
    {
        if (targetEnemy == null) return;

        Health enemyHealth = targetEnemy.GetComponent<Health>();

        if (enemyHealth == null)
        {
            targetEnemy = null;
            return;
        }

        float dist = Vector2.Distance(rb.position, targetEnemy.transform.position);

        if (dist > attackRange)
        {
            Vector2 newPos = Vector2.MoveTowards(
                rb.position,
                targetEnemy.transform.position,
                moveSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPos);
        }
        else
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Attacking target! HP: " + enemyHealth.hp);
                lastAttackTime = Time.time;
            }
        }
    }

    public void MoveTo(Vector2 pos, bool isAttackMove = false)
    {
        targetPosition = pos;
        isMoving = true;
        targetEnemy = null;
        attackMove = isAttackMove;
    }

    public void Attack(GameObject enemy)
    {
        targetEnemy = enemy;
        isMoving = false;
        attackMove = false;
    }

    public void Stop()
{
    isMoving = false;
    targetEnemy = null;
    attackMove = false;
    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
}

    GameObject FindClosestEnemy()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");

    GameObject closest = null;
    float minDist = Mathf.Infinity;

    foreach (GameObject e in enemies)
    {
        float dist = Vector2.Distance(rb.position, e.transform.position);

        if (dist < minDist)
        {
            minDist = dist;
            closest = e;
        }
    }

    foreach (GameObject b in enemyBases)
    {
        float dist = Vector2.Distance(rb.position, b.transform.position);

        if (dist < minDist)
        {
            minDist = dist;
            closest = b;
        }
    }

    return closest;
}
}