using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour, IDieable
{

    public Rigidbody2D rb;
    // Moverment
    private float moveSpeed;
    private float jumpForce;
    private float moveHorizontal;
    private bool isDead = false;
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    private bool isGrounded = true;
    float slowMove;
    // Luot
    [SerializeField] float dashBoost = 30f;
    private float dashTime;
    [SerializeField] private float DashTime;
    private bool isDashing = false;
    [SerializeField] private float dashDelay = 5;
    private float dashTimer;




    // animation
    [SerializeField] private Animator anim;
    private int currentState;
    public bool getDead()
    {
        return isDead;

    }
    public bool getGround()
    {
        return isGrounded;
    }
    public void setDead(bool isDead)
    {
        this.isDead = isDead;
    }
    //private void Awake()
    //{
    //    audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    //}
    private int GetState()
    {
        if (isDead) return Dead;
        if (!isGrounded) return Jump;
        if (isGrounded) return Mathf.Abs(moveHorizontal) > 0.1f ? Run : Idle;
        return Idle;

    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 10f;
        slowMove = moveSpeed / 3;
        jumpForce = 30f;

    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        Dash();
        var state = GetState();
        if (state != currentState)
        {
            if (currentState == Dead) return;
            anim.CrossFade(state, 0.05f, 0);
            currentState = state;

        }
        if (isDead) return;

        Move();



    }
    #region Move
    private void Dash()
    {


        if (Input.GetKeyDown(KeyCode.W) && dashTime <= 0 && !isDashing)
        {
            moveSpeed += dashBoost;
            dashTime = DashTime;
            isDashing = true;
            dashTimer = dashDelay;
        }
        if (dashTime <= 0 && isDashing)
        {
            moveSpeed -= dashBoost;
            isDashing = false;
        }
        else
        {
            dashTime -= Time.fixedDeltaTime;
        }

    }
    private void Move()
    {
        if (isGrounded && Input.GetKeyDown("space"))
        {
            rb.velocity = (new Vector2(rb.velocity.x, 1 * jumpForce));
            isGrounded = false;
        }
        // Moverment
        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            rb.velocity = (new Vector2(moveHorizontal * moveSpeed, rb.velocity.y));
            transform.rotation = Quaternion.Euler(new Vector3(0, moveHorizontal > 0 ? 0 : 180, 0));
        }
        else
        {
            float deceleration = 20.5f;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime), rb.velocity.y);
        }
    }
    #endregion
    #region Animation Keys
    private static readonly int Run = Animator.StringToHash("Player_Run");
    private static readonly int Idle = Animator.StringToHash("Player_Idle");
    private static readonly int Jump = Animator.StringToHash("Player_Jump");
    private static readonly int Dead = Animator.StringToHash("Player_Dead");
    private static readonly int Climb = Animator.StringToHash("Player_Climb");
    private static readonly int Down = Animator.StringToHash("Player_Fall");
    private static readonly int SlowDown = Animator.StringToHash("Player_SlowDown");

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}