using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("--- Movement Settings ---")]
    public float moveSpeed = 5.0F;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 lastMoveDirection = Vector2.right;

    [Header("--- Slash Settings ---")]
    public GameObject slashPrefab;
    public GameObject upSlashPrefab;
    public GameObject downSlashPrefab;
    public GameObject leftSlashPrefab;
    private GameObject attack;
    public Transform rightSpawnPoint;
    public Transform upSpawnPoint;
    public Transform downSpawnPoint;

    [Header("--- Health Settings ---")]
    public int maxHealth = 3;
    public int currentHealth = 0;

    [Header("--- On Hit Effects ---")]
    public SpriteRenderer spriteRenderer;
    public Color defaultColor = Color.white;
    public Color hitColor = Color.red;

    [Header("--- Combat Settings ---")]
    public float lungeForce = 2.0F;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PerformSlash();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleMovement()
    {
        movementInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementInput != Vector2.zero)
        {
            lastMoveDirection = movementInput.normalized;
        }

        rb.linearVelocity = movementInput * moveSpeed;

        if (movementInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementInput.x), 1, 1);
        }
    }

    void PerformSlash()
    {
        rb.linearVelocity = Vector2.zero;
        attack = null;

        if (lastMoveDirection.x > 0)
        {
            attack = Instantiate(slashPrefab, rightSpawnPoint.position, Quaternion.identity);
        }
        else if (lastMoveDirection.x < 0)
        {
            attack = Instantiate(leftSlashPrefab, rightSpawnPoint.position, Quaternion.identity);
        }
        else if (lastMoveDirection.y > 0)
        {
            attack = Instantiate(upSlashPrefab, upSpawnPoint.position, Quaternion.identity);
        }
        else if (lastMoveDirection.y < 0)
        {
            attack = Instantiate(downSlashPrefab, downSpawnPoint.position, Quaternion.identity);
        }

        attack.transform.right = lastMoveDirection;

        CameraShake.instance.Shake(0.1F, 0.1F);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CameraShake.instance.Shake(0.2F, 0.2F);
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
        SceneManager.LoadScene("GameOver");
    }
}