using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("--- Movement Settings ---")]
    public float moveSpeed = 5.0F;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("--- Slash Settings ---")]
    public GameObject slashPrefab;
    public Transform slashSpawnPoint;

    [Header("--- Health Settings ---")]
    public int maxHealth = 3;
    public int currentHealth = 0;

    [Header("--- On Hit Effects ---")]
    public SpriteRenderer spriteRenderer;
    public Color defaultColor = Color.white;
    public Color hitColor = Color.red;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PerformSlash();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void PerformSlash()
    {
        Instantiate(slashPrefab, slashSpawnPoint.position, transform.rotation);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ChangeColor(hitColor);
        Debug.Log($"Player Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
        StartCoroutine(RevertColor());
    }

    IEnumerator RevertColor()
    {
        yield return new WaitForSeconds(0.2F);
        spriteRenderer.color = defaultColor;
    }

    void Die()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}