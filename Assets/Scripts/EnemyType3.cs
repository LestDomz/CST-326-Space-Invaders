using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : MonoBehaviour
{
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    // Start is called before the first frame update
    [System.Obsolete]
    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Ouch!");
      Destroy(collision.gameObject);
      Destroy(gameObject);

        OnEnemyDied?.Invoke(300);
        FindObjectOfType<EnemyManager>().EnemyDestroyed();
    }
}
