using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float upRotation = 30f;
    [SerializeField] private float downRotation = -90f;
    [Header("Audio")]
    [SerializeField] private AudioClip hitClip;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator animator;
    private PlayerInput playerInput;
    private ScoreManager scoreManager;
    private GameManager gameManager;

    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
        rb.gravityScale = 0;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Tap.performed += ctx => Jump();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnDisable()
    {
        playerInput.Player.Tap.performed -= ctx => Jump();
    }

    private void Jump()
    {   
        if (!gameManager.IsGameStarted())
        {
            rb.gravityScale = 2;
            gameManager.StartGame();
        }

        if (Time.timeScale == 0 || !gameManager.IsGameActive() || IsPointerOverUIObject())
            return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        transform.eulerAngles = new Vector3(0, 0, upRotation);
        audioSource.Play();
    }

    private void Update()
    {
        if (rb.velocity.y < 0)
            RotatePlayer(downRotation);

        if (transform.position.y > Camera.main.orthographicSize)
            Die();
    }

    private void RotatePlayer(float targetRotation)
    {
        float currentRotation = transform.eulerAngles.z;
        float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, newRotation);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Score")
        {
            scoreManager.AddScore(1);
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Die();
    }

    private void Die()
    {
        if (isDead)
            return;
        
        isDead = true;
        audioSource.PlayOneShot(hitClip);
        animator.SetTrigger("Die");
        gameManager.FinishGame();
        scoreManager.GameOverScore();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            eventData.position = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            eventData.position = Mouse.current.position.ReadValue();
        }
        else
        {
            return false;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "IgnoreUI")
                continue;

            return true;
        }

        return false;
    }
}