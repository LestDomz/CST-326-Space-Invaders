using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform enemyContainer; // Parent object holding all enemies
    public float moveSpeed = 1.0f;   // Base horizontal speed
    public float speedIncreaseFactor = 0.1f; // How much speed increases per enemy destroyed
    public float maxMoveSpeed = 5.0f; // Limit to prevent excessive speed
    public float moveDownAmount = 0.5f; // Distance to move downward
    public float moveDelay = 0.5f;   // Initial movement delay
    public float minMoveDelay = 0.1f; // The fastest possible movement speed
    public float boundaryX = 5.0f;   // Left/right movement boundary

    private int direction = 1; // 1 = right, -1 = left
    private int totalEnemies;  // Track the total number of enemies

    void Start()
    {
        totalEnemies = enemyContainer.childCount; // Get initial enemy count
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        while (enemyContainer.childCount > 0) // Keep moving while enemies exist
        {
            // Move enemies horizontally
            enemyContainer.position += Vector3.right * moveSpeed * direction;

            // Check if any enemy reaches a boundary
            foreach (Transform enemy in enemyContainer)
            {
                if (Mathf.Abs(enemy.position.x) >= boundaryX)
                {
                    MoveDown();
                    break; // Stop checking after one enemy triggers movement
                }
            }

            yield return new WaitForSeconds(moveDelay); // Wait before next move
        }
    }

    void MoveDown()
    {
        direction *= -1; // Reverse direction
        enemyContainer.position += Vector3.down * moveDownAmount; // Move downward
    }

    public void EnemyDestroyed()
    {
        int remainingEnemies = enemyContainer.childCount;

        // Speed up as more enemies are destroyed
        float speedMultiplier = 1.0f + ((totalEnemies - remainingEnemies) * 0.05f);
        moveDelay = Mathf.Max(0.5f / speedMultiplier, minMoveDelay); // Decrease delay
        moveSpeed = Mathf.Min(moveSpeed + speedIncreaseFactor, maxMoveSpeed); // Increase horizontal speed
    }
}
