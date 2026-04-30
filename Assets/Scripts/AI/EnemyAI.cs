using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float damage = 10f;
    public float attackCooldown = 1f;

    private GameObject target;
    private float lastAttackTime;

    void Update()
    {
        FindClosestPlayerUnit();

        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(
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

    void FindClosestPlayerUnit()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Unit");

        float closestDistance = Mathf.Infinity;
        GameObject closestUnit = null;

        foreach (GameObject unit in playerUnits)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = unit;
            }
        }

        target = closestUnit;
    }

    void AttackTarget()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            Health health = target.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Enemy attacking player! Player HP: " + health.hp);
            }

            lastAttackTime = Time.time;
        }
    }
}