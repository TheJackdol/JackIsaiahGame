using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI hudText;

    public Health playerBaseHealth;
    public Health enemyBaseHealth;
    public ResourceManager resourceManager;

    void Update()
    {
        int playerUnits = GameObject.FindGameObjectsWithTag("Unit").Length;

        string playerHP = playerBaseHealth != null ? playerBaseHealth.hp.ToString("0") : "Destroyed";
        string enemyHP = enemyBaseHealth != null ? enemyBaseHealth.hp.ToString("0") : "Destroyed";

        string resourcesText = resourceManager != null ? resourceManager.resources.ToString() : "0";

hudText.text =
    "Resources: " + resourcesText +
    "\nUnits: " + playerUnits +
    "\nPlayer Base HP: " + playerHP +
    "\nEnemy Base HP: " + enemyHP +
    "\n\nQ = Stop" +
    "\nZ = Attack Move" +
    "\nE = Attack";
    }
}