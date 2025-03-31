using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstanceRealWorld : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Rigidbody2D rig;
    private Animator animator;
    private Vector2 direction = Vector2.zero;
    public Vector2 constancePosition;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!Dialog.dialogOpen)
        {
            ProcessInputs();
            Animate();
        }
        constancePosition = transform.position;
        //JUST for trying all the levels quickly
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("lv1");
        }else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("lv2");
        }else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("lv3");
        }else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("lv4");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;
    }

    private void Move()
    {
        rig.velocity = new Vector2(direction.x * velocidad, direction.y * velocidad);
    }

    private void Animate()
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("XInput", direction.x);
            animator.SetFloat("YInput", direction.y);
        }
        animator.SetFloat("WalkMagnitude", direction.magnitude);
    }
}
