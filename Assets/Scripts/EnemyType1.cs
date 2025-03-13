using System.Collections;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;
    public Animator animator;
    public float Death = 1f;
    private AudioSource hitSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PlayDeathAnimation());

            OnEnemyDied?.Invoke(250);
            FindObjectOfType<EnemyManager>().EnemyDestroyed("EnemyType1");
        }
    }

    IEnumerator PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(Death);
        hitSound.Play();
        Destroy(gameObject);
    }
}
