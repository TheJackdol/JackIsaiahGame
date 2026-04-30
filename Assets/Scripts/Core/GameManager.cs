using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerBase;
    public GameObject enemyBase;

    void Update()
    {
        if (enemyBase == null)
        {
            Debug.Log("YOU WIN!");
            Time.timeScale = 0f;
        }

        if (playerBase == null)
        {
            Debug.Log("YOU LOSE!");
            Time.timeScale = 0f;
        }
    }
}