using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform spawnPoint;
    public int unitCost = 25;
    public ResourceManager resourceManager;

    public void SpawnUnit()
{
    if (resourceManager != null && resourceManager.CanAfford(unitCost))
    {
        Vector2 spawnOffset = Random.insideUnitCircle * 0.5f;

        Instantiate(unitPrefab, (Vector2)spawnPoint.position + spawnOffset, Quaternion.identity);

        resourceManager.Spend(unitCost);
        Debug.Log("Unit spawned!");
    }
    else
    {
        Debug.Log("Not enough resources!");
    }
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnUnit();
        }
    }
}