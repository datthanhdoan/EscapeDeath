using UnityEngine;

public class PlayerMoverment : MonoBehaviour, IDieable
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask climbLayer;
    private AudioManagerScript audioManagerScript;
    // Moverment
    private float moveSpeed;
    private float jumpForce;
    private float moveHorizontal;
    private bool isDead = false;
    private bool isFacingRight = true;
    private bool isShift = false;
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    private Transform characterTransform;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 defaultColliderSize;
    public float ShiftedColliderHeight = 0.7f;
    // Climd , ladder
    private bool isClimb = false;
    private float width, height;
    private float numberOfRays = 5;
    private float spacingRays;
    private bool isGrounded = true;
    float slowMove;
    // Effect
    [SerializeField] ParticleSystem partical;
    private bool hasBloodEffectPlayed = false;



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
    public bool getClimb()
    {
        return isClimb;
    }
    public bool getShift()
    {
        return isShift;
    }
    public void setDead(bool isDead)
    {
        this.isDead = isDead;
    }
    private void Awake()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }
    private int GetState()
    {
        if (isDead)
        {
            return Dead;
        }
        if (isClimb)
        {
            return Climb;
        }
        if (!isGrounded) return Jump;
        if (isGrounded && isShift && Mathf.Abs(moveHorizontal) > 0.1f)
        {
            return SlowDown;
        }
        if (isGrounded && isShift) return Down;
        if (isGrounded) return Mathf.Abs(moveHorizontal) > 0.1f ? Run : Idle;
        return Idle;

    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterTransform = transform;
        moveSpeed = 450f;
        slowMove = moveSpeed / 3;
        jumpForce = 25f;
        width = GetComponent<CapsuleCollider2D>().size.x;
        height = GetComponent<CapsuleCollider2D>().size.y;
        spacingRays = width / (numberOfRays - 1);
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        defaultColliderSize = capsuleCollider.size;
    }

    void Update()
    {
        isShift = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? true : false;
        // State animation
        var state = GetState();
        if (state != currentState)
        {
            if (currentState == Dead) return;
            anim.CrossFade(state, 0.05f, 0);
            currentState = state;

        }
        if (isDead)
        {
            if (!hasBloodEffectPlayed)
            {
                audioManagerScript.SoundEffect(audioManagerScript.deadth);
                partical.Play();
                hasBloodEffectPlayed = true;
            }
            return;
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        checkOnGround();
        checkAHead();
        if (isGrounded && Input.GetKeyDown("space"))
        {
            rb.velocity = (new Vector2(rb.velocity.x, 1 * jumpForce));
            isGrounded = false;
        }
        if (moveHorizontal > 0f) isFacingRight = true;
        if (moveHorizontal < 0f) isFacingRight = false;

        // Moverment
        if (Mathf.Abs(moveHorizontal) > 0.1f && !isShift)
        {
            rb.velocity = (new Vector2(moveHorizontal * Time.fixedDeltaTime * moveSpeed, rb.velocity.y));
            transform.rotation = Quaternion.Euler(new Vector3(0, moveHorizontal > 0 ? 0 : 180, 0));
        }
        else
        {
            float deceleration = 13.5f;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime), rb.velocity.y);
        }
        // Moverment --- hold shift
        //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        //{
        //    capsuleCollider.size = new Vector2(defaultColliderSize.x, ShiftedColliderHeight);
        //}
        //else
        //{
        //    capsuleCollider.size = defaultColliderSize;
        //}
        if (Mathf.Abs(moveHorizontal) > 0.1f && isShift)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, moveHorizontal > 0 ? 0 : 180, 0));
            rb.velocity = (new Vector2(moveHorizontal * Time.fixedDeltaTime * slowMove, rb.velocity.y));
        }
        else
        {
            float deceleration = 13.5f;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime), rb.velocity.y);
        }
        // Moverment --- move up when climbing
        if (isClimb)
        {
            rb.velocity = (new Vector2(0f, 5f));
        }
    }

    private void FixedUpdate()
    {
        // for raycast direction

        //if (isClimb)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 3f);
        //}

    }
    #region Animation Keys
    private static readonly int Run = Animator.StringToHash("Player_Run");
    private static readonly int Idle = Animator.StringToHash("Player_Idle");
    private static readonly int Jump = Animator.StringToHash("Player_JumpIn");
    private static readonly int Dead = Animator.StringToHash("Player_Dead");
    private static readonly int Climb = Animator.StringToHash("Player_Climb");
    private static readonly int Down = Animator.StringToHash("Player_Down");
    private static readonly int SlowDown = Animator.StringToHash("Player_SlowDown");

    #endregion
    private void checkOnGround()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x - width / 2, transform.position.y);

        bool anyRaycastHit = false;

        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, height / 2 + 0.1f, groundLayer);
            Debug.DrawRay(rayOrigin, Vector2.down * (height / 2 + 0.1f), Color.blue);
            rayOrigin.x += spacingRays;

            if (hit.collider != null)
            {
                anyRaycastHit = true;
                break;
            }
        }

        isGrounded = anyRaycastHit;
    }




    private void checkAHead()
    {
        //bool checkDone = false;
        Vector2 rayOrigin = isFacingRight
            ? new Vector2(transform.position.x + width / 2, transform.position.y - height / 2)
            : new Vector2(transform.position.x - width / 2, transform.position.y - height / 2);

        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        bool anyRaycastHit = false;
        int index = 0;
        // them 5 raycast cho chieu cao 
        for (int i = 0; i < numberOfRays - 2; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, spacingRays, climbLayer);
            Debug.DrawRay(rayOrigin, direction * spacingRays, Color.blue);
            rayOrigin.y += height / (numberOfRays + 5 - 1);
            if (hit.collider != null)
            {
                anyRaycastHit = true;
                break;
            }
            index++;

        }

        //  RaycastHit2D hitcap = Physics2D.CapsuleCast(transform.position, new Vector2(0.6f, 2), new CapsuleDirection2D(Vector2.one), 0, Vector2.right);

        isClimb = anyRaycastHit;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Mob"))
    //    {
    //        isDead = true;
    //        Debug.Log("Player DEad");
    //    }
    //}


}