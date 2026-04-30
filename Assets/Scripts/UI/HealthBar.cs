using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Transform bar;

    void Update()
{
    if (health != null)
    {
        float ratio = health.hp / 100f;

        Vector3 scale = bar.localScale;
        scale.x = ratio;     // only change width
        bar.localScale = scale;
    }
}
}