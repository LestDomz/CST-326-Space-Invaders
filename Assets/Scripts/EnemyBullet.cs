using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensure Rigidbody2D exists
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public float speed = 5;
    public Vector2 direction = Vector2.up; // Default: move up

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
    }

    private void Fire()
    {
        myRigidbody2D.linearVelocity = direction * speed; // Move in the assigned direction
        Debug.Log("Bullet fired!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy bullet on impact
        }
    }
}
