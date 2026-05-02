using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int resources = 100;
    public int incomePerTick = 5;
    public float incomeDelay = 2f;

    private float lastIncomeTime;

    void Update()
    {
        if (Time.time > lastIncomeTime + incomeDelay)
        {
            resources += incomePerTick;
            lastIncomeTime = Time.time;
        }
    }

    public bool CanAfford(int cost)
    {
        return resources >= cost;
    }

    public void Spend(int cost)
    {
        resources -= cost;
    }
}