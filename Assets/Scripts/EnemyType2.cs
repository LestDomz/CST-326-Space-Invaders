using System.Collections;
using UnityEngine;

public class EnemyType2 : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign in Unity Inspector
    public Transform bulletSpawnPoint; // Assign a spawn point under the enemy
    public float minShootTime = 2.0f; // Minimum time between shots
    public float maxShootTime = 5.0f; // Maximum time between shots
    public float bulletSpeed = 5.0f; // Speed of enemy bullets

    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootAtIntervals());
    }

    IEnumerator ShootAtIntervals()
    {
        while (true)
        {
            float waitTime = Random.Range(minShootTime, maxShootTime);
            yield return new WaitForSeconds(waitTime);

            if (player != null)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

    [System.Obsolete]
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        Destroy(collision.gameObject);
        Destroy(gameObject);

        OnEnemyDied?.Invoke(150);
        FindObjectOfType<EnemyManager>().EnemyDestroyed();
    }
}