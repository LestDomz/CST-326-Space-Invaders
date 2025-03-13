using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene transitions

public class EnemyManager : MonoBehaviour
{
    public int enemyType1Count = 0;
    public int enemyType2Count = 0;
    public int enemyType3Count = 0;
    public int enemyType4Count = 0;

    public Transform enemyContainer; // Parent holding all enemies
    public float moveSpeed = 1.0f;
    public float moveDownAmount = 0.5f;
    public float moveDelay = 0.5f;
    public float minMoveDelay = 0.1f;
    public float boundaryX = 5.0f;

    private int direction = 1; // 1 = right, -1 = left
    private int totalEnemies;

    void Start()
    {
        // Count how many of each enemy type are in the scene at the start
        enemyType1Count = FindObjectsOfType<EnemyType1>().Length;
        enemyType2Count = FindObjectsOfType<EnemyType2>().Length;
        enemyType3Count = FindObjectsOfType<EnemyType3>().Length;
        enemyType4Count = FindObjectsOfType<EnemyType4>().Length;

        Debug.Log($"Enemies at start -> Type1: {enemyType1Count}, Type2: {enemyType2Count}, Type3: {enemyType3Count}, Type4: {enemyType4Count}");
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        while (enemyContainer.childCount > 0) // Keep moving while enemies exist
        {
            enemyContainer.position += Vector3.right * moveSpeed * direction;

            foreach (Transform enemy in enemyContainer)
            {
                if (Mathf.Abs(enemy.position.x) >= boundaryX)
                {
                    MoveDown();
                    break;
                }
            }

            yield return new WaitForSeconds(moveDelay);
        }
    }

    void MoveDown()
    {
        direction *= -1;
        enemyContainer.position += Vector3.down * moveDownAmount;
    }

    public void EnemyDestroyed(string enemyType)
    {
        // Reduce the count based on enemy type
        if (enemyType == "EnemyType1") enemyType1Count--;
        else if (enemyType == "EnemyType2") enemyType2Count--;
        else if (enemyType == "EnemyType3") enemyType3Count--;
        else if (enemyType == "EnemyType4") enemyType4Count--;

        Debug.Log($"Remaining Enemies -> Type1: {enemyType1Count}, Type2: {enemyType2Count}, Type3: {enemyType3Count}, Type4: {enemyType4Count}");

        // If all enemy counts reach 0, transition to credits
        if (enemyType1Count <= -6 && enemyType2Count <= 2 && enemyType3Count <= 2 && enemyType4Count <= 2)
        {
            Debug.Log("All enemy types destroyed! Transitioning to Credits...");
            Invoke("LoadCreditsScene", 1f);
        }
    }

    IEnumerator CheckAllEnemiesDefeated()
    {
        yield return new WaitForSeconds(0.1f); // Small delay for Unity to update the hierarchy

        int activeEnemies = 0;
        foreach (Transform enemy in enemyContainer)
        {
            if (enemy.childCount > 0) // If it has children, it's an enemy type, skip it
                continue;

            activeEnemies++; // Count only active enemies
        }

        Debug.Log("Remaining Active Enemies: " + activeEnemies);

        if (activeEnemies == 0) // If no active enemies remain, transition to credits
        {
            Debug.Log("All enemies defeated! Transitioning to Credits...");
            Invoke("LoadCreditsScene", 1f);
        }
    }


    void LoadCreditsScene()
    {
        Debug.Log("Loading Credits Scene...");
        SceneManager.LoadScene("Credits"); // Load the Credits scene
    }
}
