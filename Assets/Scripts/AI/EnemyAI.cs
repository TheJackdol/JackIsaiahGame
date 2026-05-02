using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 2.5f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    private GameObject target;
    private float lastAttackTime;

    void Update()
    {
        target = FindClosestTarget();

        if (target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            AttackTarget();
        }
    }

    GameObject FindClosestTarget()
    {
        Health[] allTargets = FindObjectsOfType<Health>();

        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Health h in allTargets)
        {
            GameObject obj = h.gameObject;

            if (obj == gameObject) continue;
            if (obj.CompareTag("Enemy")) continue;
            if (obj.CompareTag("EnemyBase")) continue;

            float distance = Vector2.Distance(transform.position, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }

    void AttackTarget()
    {
        Health health = target.GetComponent<Health>();

        if (health == null)
        {
            target = null;
            return;
        }

        if (Time.time > lastAttackTime + attackCooldown)
        {
            health.TakeDamage(damage);
            Debug.Log("Enemy attacking: " + target.name + " HP: " + health.hp);
            lastAttackTime = Time.time;
        }
    }
}