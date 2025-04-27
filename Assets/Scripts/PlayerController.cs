using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("--- Movement Settings ---")]
    public float moveSpeed = 5.0F;
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

    [Header("--- Effect Settings ---")]
    public GameObject footstepEffectPrefab;
    public Transform footTransform;
    public float stepInterval = 0.5F;
    private float stepTimer = 0.0F;

    [Header("--- Dash Settings ---")]
    public float dashSpeed = 10.0F;
    public float dashDuration = 0.2F;
    public float dashCooldown = 1.0F;
    public float iFramesDuration = 0.2F;
    private float dashTime = 0.0F;
    private float cooldownTime = 0.0F;
    private bool isDashing = false;
    private bool isInvincible = false;
    private Vector2 dashDirection;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleMovement();
        HandleEffects();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleDash();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PerformSlash();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

        if (isDashing)
        {
            rb.linearVelocity = movementInput.normalized * dashSpeed;
        }
        else
        {
            rb.linearVelocity = movementInput.normalized * moveSpeed;
        }
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

        bool isMoving = movementInput != Vector2.zero;
        anim.SetBool("isMoving", isMoving);
    }

    void HandleEffects()
    {
        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0F && movementInput.x > 0 || movementInput.x < 0 || movementInput.y > 0 || movementInput.y < 0)
        {
            CreateFootstepEffect();
            stepTimer = stepInterval;
        }
    }

    void CreateFootstepEffect()
    {
        GameObject footstep = Instantiate(footstepEffectPrefab, footTransform.position, Quaternion.identity);
        StartCoroutine(DestroyFootstepEffect(footstep));
    }

    IEnumerator DestroyFootstepEffect(GameObject footstep)
    {
        ParticleSystem particleSystem = footstep.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            yield return new WaitForSeconds(particleSystem.main.duration);
        }
        else
        {
            yield return new WaitForSeconds(1.0F);
        }

        Destroy(footstep);
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

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTime <= 0F && !isDashing)
        {
            StartDash();
        }

        if (cooldownTime > 0F)
        {
            cooldownTime -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;

            if (dashTime <= 0F)
            {
                StopDash();
            }
            else
            {
                rb.linearVelocity = dashDirection * dashSpeed;
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        isInvincible = true;
        dashTime = dashDuration;
        cooldownTime = dashCooldown;

        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void StopDash()
    {
        isDashing = false;
        isInvincible = false;
        rb.linearVelocity = Vector2.zero;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}