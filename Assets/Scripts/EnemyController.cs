using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [Header("--- Movement Settings ---")]
    public float moveSpeed = 2.0F;

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
        }
    }
}