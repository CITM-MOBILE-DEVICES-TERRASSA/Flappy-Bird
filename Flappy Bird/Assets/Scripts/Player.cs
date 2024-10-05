using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float upRotation = 30f;
    [SerializeField] private float downRotation = -90f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private PlayerInput playerInput;
    private ScoreManager scoreManager;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
        rb.gravityScale = 0;

        audioSource = GetComponent<AudioSource>();

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
            rb.gravityScale = 1;
            gameManager.StartGame();
        }

        if (Time.timeScale == 0 || IsPointerOverUIObject())
            return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        transform.eulerAngles = new Vector3(0, 0, upRotation);
        audioSource.Play();
    }

    private void Update()
    {
        if (rb.velocity.y < 0)
            RotatePlayer(downRotation);
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

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
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