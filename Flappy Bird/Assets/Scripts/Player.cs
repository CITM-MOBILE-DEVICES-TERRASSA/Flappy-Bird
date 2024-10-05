using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float upRotation = 30f;
    [SerializeField] private float downRotation = -90f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private PlayerInput playerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Tap.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerInput.Player.Tap.performed -= ctx => Jump();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        transform.eulerAngles = new Vector3(0, 0, upRotation);
        audioSource.Play();
    }

    void Update()
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
}
