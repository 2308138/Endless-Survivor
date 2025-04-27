using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [Header("--- Movement Settings ---")]
    public float moveSpeed = 2.0F;
    public int currentHealth = 1;

    public AudioSource audioSource;
    public AudioClip deathClip;
    public SpriteRenderer sprite;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            if (direction.x > 0F)
            {
                if (direction.x > 0.1F)
                    sprite.flipX = false;
                else if (direction.x < 0.1F)
                    sprite.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            GetComponent<AudioSource>().PlayOneShot(deathClip);
            Destroy(gameObject);
        }
    }
}