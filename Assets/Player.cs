using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerInputAction playerControls;
    private InputAction move;
    private InputAction jump;
    Vector2 moveDirection = Vector2.zero;
    float moveDirectionY;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private int jumping = 0;
    Vector2 jumpDirection = Vector2.zero;
    private int jumpCount = 0;
    private bool ground = true;

    private void Awake()
    {
        playerControls = new PlayerInputAction();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jumping;
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirectionY * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        if (jumpCount < 2f && jumping < 2f)
        {
            StartCoroutine(JumpCoroutine());
            Debug.Log("Jump");
        }
    }

    IEnumerator JumpCoroutine()
    {
        // Increment the jump count
        jumpCount++;

        // Apply the jump force
        moveDirectionY = CalculateJumpVerticalSpeed(); //Mathf.Sqrt(2f * 15f);
        yield return new WaitForSeconds(0.1f); // Adjust this delay if needed
        moveDirectionY = 0f;

        // Wait for a short duration to prevent consecutive jumps
        yield return new WaitForSeconds(0.1f);

        jumping++;

        jumpCount--;

        // Decrement the jump count after the wait duration
    }

    private float CalculateJumpVerticalSpeed()
    {
        // Modify this method to adjust the jump height
        return Mathf.Sqrt(2f * 1.5f * Mathf.Abs(Physics.gravity.y));
    }


    public void ResetJump(bool touchGround)
    {
        if (touchGround)
        {
            jumping = 0;
        }
        ground = touchGround;
    }
}
