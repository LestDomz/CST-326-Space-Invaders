using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shottingOffset;
    Animator playerAnimator;
    private AudioSource hitSound;

    public float maxPlayerSpeed = 10.0f;
    private void Start()
    {
        EnemyType1.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType2.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType3.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType4.OnEnemyDied += EnemyOnOnEnemyDied;
        playerAnimator = GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        EnemyType1.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType2.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType3.OnEnemyDied += EnemyOnOnEnemyDied;
        EnemyType4.OnEnemyDied += EnemyOnOnEnemyDied;
    }
    void EnemyOnOnEnemyDied(int points)
    {
        Debug.Log($"I know about dead enemy, points: {points}");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = 0f;

        horizontalAmount = Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position + new Vector3(horizontalAmount * maxPlayerSpeed * Time.deltaTime, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);

        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            playerAnimator.SetTrigger("Shoot Trigger");

            GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
            Debug.Log("Bang!");
            hitSound.Play();

            //Destroy(shot, 3f);

        }
    }
}