using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float jumpForce = 15f;
    public Animator animator;
    private Rigidbody2D rigidbody;
    
    private Vector2 posicionInicial;    
    
    bool IsJumping = false;
    bool isGrounded = false;

    bool Dragging = false;

    bool hasDoubleJumped = false;

    private float baseGravity;
    private bool canDash = true;
    [SerializeField]private float dashingPower = 20f;
    private bool isDashing;
    [SerializeField]private float dashCooldown = 2f;
    [SerializeField]private float dashingTime = 0.2f;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private AudioSource drag;

    [SerializeField] private AudioSource dash;

    [SerializeField] private AudioSource jump;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseGravity = rigidbody.gravityScale;
        tr = GetComponent<TrailRenderer>();
        posicionInicial = rigidbody.position;
       
    }
    void Update()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        ProcesarMovimiento(inputMovimiento);

        animator.SetFloat("Velocidad", Mathf.Abs(inputMovimiento));

        if (Input.GetMouseButtonDown(0))
        {
            // Left mouse button is pressed
            drag.Play();
            animator.SetBool("Dragging", true);
        }
        else
        {
            // Left mouse button is released
            animator.SetBool("Dragging", false);
        }


    }


    void ProcesarMovimiento(float inputMovimiento)
    {
        if (isDashing == true)
        {   
            return;
        }
        // Flip the sprite based on the direction
        if (inputMovimiento > 0)
        {
            // Moving right, keep the original scale
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputMovimiento < 0)
        {
            // Moving left, flip the sprite horizontally
            
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // If inputMovimiento is 0, the player is not moving horizontally, so you may choose to keep the current orientation.

        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            if (isGrounded)
            {   
                jump.Play();
                Jump();
            }
            else if (!hasDoubleJumped)
            {   
                jump.Play();
                DoubleJump();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dash.Play();
            StartCoroutine(Dash());
        }
        // Set the velocity
        if (!isDashing)
        {
            rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        }
        
    }

    void Jump()
    {

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        isGrounded = false;
        IsJumping = true;
        animator.SetBool("IsJumping", true);


    }

    void DoubleJump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        hasDoubleJumped = true;
    }

    // Check if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.CompareTag("return"))
        {
           rigidbody.position = posicionInicial;
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        isGrounded = true;
        IsJumping = false;
        hasDoubleJumped = false;
        animator.SetBool("IsJumping", false);

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rigidbody.gravityScale = 0;
        rigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rigidbody.gravityScale = baseGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}