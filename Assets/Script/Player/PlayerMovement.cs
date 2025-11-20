using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region movement
    private Rigidbody2D rb; //Acceder a las fisicas
    private NewActions playerInput; //Acceder al new input system creado
    private Vector2 input; //Variable tipo vector 2 para guardar los movimientos
    [SerializeField]
    private float move_speed = 8f; //Variable para la velocidad
    #endregion
    #region Jump
    [SerializeField]
    private float forceJump = 10f; //Fuerza de salto
    private int saltosMaximos = 2; //Cantidad de saltos
    private int saltosDisponibles;
    private bool isGround = true;
    #endregion
    #region Dash
    [SerializeField]
    private float dashSpeed = 14f;
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private float dashCooldown = 0.2f;
    bool isDashing = false;
    float lastDirection = 1f;

    #endregion
    public Animator anim;
    private SpriteRenderer sr;
    private Vector2 initialPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new NewActions();
        saltosDisponibles = saltosMaximos;
        initialPosition = rb.transform.position;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OffMove;
        playerInput.Player.Jump.performed += Jump;
        playerInput.Player.Dash.performed += Dash;
        playerInput.Player.Attack.performed += Attack;
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OffMove;
        playerInput.Player.Jump.performed -= Jump;
        playerInput.Player.Dash.performed -= Dash;
        playerInput.Player.Disable();

    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }

    private void OffMove(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (saltosDisponibles > 0 && isGround)
        {
            rb.linearVelocityY = 0;
            rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            saltosDisponibles -= 1;
        }
    }

    private void Dash(InputAction.CallbackContext ctx)
    {
        if (!isDashing)
        {
            StartCoroutine(DashRoutine());
        }

    }

    private void Attack(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Attack");
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(dashSpeed * lastDirection, 0);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            saltosDisponibles = saltosMaximos;
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && saltosDisponibles == saltosMaximos)
        {
            isGround = false;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MagicDoor"))
        {
            SceneManager.LoadScene("Game_Level_2");
        }
        if (collision.CompareTag("FlagWin"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing && input.x != 0)
        {
            lastDirection = Mathf.Sign(input.x);
        }

        if (isDashing)
        {
            return;
        }

        float newVelocityX = input.x * move_speed;

        float currentVelocityY = rb.linearVelocity.y;

        rb.linearVelocity = new Vector2(newVelocityX, currentVelocityY);

        if (input.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (input.x > 0)
        {
            sr.flipX = false;   
        }
        else if (input.x < 0)
        {
            sr.flipX = true;    
        }


    }

    public void ResetPlayer()
    {
        gameObject.transform.position = initialPosition;
    }
}
