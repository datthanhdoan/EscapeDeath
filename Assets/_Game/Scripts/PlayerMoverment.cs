using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    private AudioManagerScript audioManagerScript;
    public Rigidbody2D rb;
    // Moverment
    public LayerMask groundLayer;
    private float moveSpeed;
    private float jumpForce;
    private float moveHorizontal, moveVertical;
    private float originalGravity;
    private bool isDead = false;
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    public int points = 0;
    private bool isGrounded;
    float slowMove;
    // Luot
    [SerializeField] float dashBoost = 30f;
    private float dashTime;
    [SerializeField] private float DashTime;
    private bool isDashing = false;
    [SerializeField] private float dashDelay = 5;
    private float dashTimer;

    //Effect
    [Header("Effect")]
    [SerializeField] private TrailRenderer _dash;
    private float _timeCoolDownDash = 1f;
    private float _TimerCoolDownDash = 0f;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private GameObject _floatingPoint;
    // Combat
    [Header("Combat")]
    private bool _isTakeHit = false;
    private bool _isCombat = false;
    private int _CombatState;
    private bool _isCombatComplete = true;
    public Transform _attackPoint;
    public int _attackDamage = 20;
    [Range(0, 5)] public float _attackRange;
    [SerializeField] private LayerMask _enemyLayers;
    private float _timeCoolDownAttack = 0.28f;
    private float _TimerCoolDownAttack = 0f;
    private float _timeCoolDownDartAttack = 1f;
    private float _TimerCoolDownDartAttack = 0f;
    public GameObject DartObject;


    //
    [Header("Health")]
    public PlayerHealthBar _playerHealthBar;
    public int _maxHealth = 150;
    public int _currentHealth;

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
    private void Awake()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }
    private int GetState()
    {
        if (isDead) return Dead;
        if (_isCombat) return _CombatState;
        if (!isGrounded) return Jump;
        if (isGrounded) return Mathf.Abs(moveHorizontal) > 0.1f ? Run : Idle;
        return Idle;
    }


    void Start()
    {
        _currentHealth = _maxHealth;
        _playerHealthBar.SetHealth(_currentHealth, _maxHealth);
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 10f;
        slowMove = moveSpeed / 3;
        jumpForce = 30f;
        originalGravity = rb.gravityScale;

    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        checkOnGround();
        Combat();
        DashEffect();
        var state = GetState();
        if (state != currentState)
        {
            if (currentState == Dead) return;
            anim.CrossFade(state, 0.05f, 0);
            currentState = state;
        }
        if (isDead) return;
        if (!_isCombatComplete) moveHorizontal = moveVertical = 0;

        Move();
    }

    #region Effect
    private void FloatingPoints(int damage)
    {
        GameObject points = Instantiate(_floatingPoint, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
    }
    private void DashEffect()
    {
        if (_TimerCoolDownDash <= 0)
        {
            if (Input.GetKeyDown(KeyCode.W) && dashTime <= 0 && !isDashing)
            {
                _TimerCoolDownDash = _timeCoolDownDash;
                moveSpeed += dashBoost;
                dashTime = DashTime;
                isDashing = true;
                dashTimer = dashDelay;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                _dash.emitting = true;
                if (isGrounded) DustEffect();
            }
        }
        else
        {
            _TimerCoolDownDash -= Time.deltaTime;
        }
        if (dashTime <= 0 && isDashing)
        {
            moveSpeed -= dashBoost;
            isDashing = false;
            rb.gravityScale = originalGravity;
            _dash.emitting = false;

        }
        else
        {
            dashTime -= Time.fixedDeltaTime;
        }

    }

    private void DustEffect()
    {
        _dust.Play();
    }

    #endregion


    #region ComBat
    private bool CheckAnimation(string animationName)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
    private void Combat()
    {
        // Can toi uu lai
        if (_isTakeHit)
        {
            _isCombatComplete = false;
            _isCombat = true;
            _CombatState = TakeHit;
            return;
        }


        if (_TimerCoolDownAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _isCombatComplete = false;
                _isCombat = true;
                _CombatState = Random.Range(0, 2) == 0 ? AT1 : AT2;
                BasicAttack();
                _TimerCoolDownAttack = _timeCoolDownAttack;
                return;
            }

        }
        else
        {
            _TimerCoolDownAttack -= Time.deltaTime;
        }
        if (_TimerCoolDownDartAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _isCombatComplete = false;
                _isCombat = true;
                _CombatState = Dart;
                DartAttack();
                _TimerCoolDownDartAttack = _timeCoolDownDartAttack;
                return;
            }
        }
        else
        {
            _TimerCoolDownDartAttack -= Time.deltaTime;
        }

        if (_isCombat)
        {
            foreach (string animationName in combatAnimations)
            {
                if (CheckAnimation(animationName))
                {
                    _isCombatComplete = true;
                    break;
                }
            }
        }
        if (_isCombatComplete) _isCombat = false;
    }
    private void SkillCoolDown()
    {

    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _playerHealthBar.SetHealth(_currentHealth, _maxHealth);
        _isCombat = true;
        FloatingPoints(damage);
        if (_currentHealth <= 0)
        {
            isDead = true;
        }
    }
    public void TakeHealth(int health)
    {

        _currentHealth = (_currentHealth + health) > _maxHealth ? _maxHealth : (_currentHealth + health);
        _playerHealthBar.SetHealth(_currentHealth, _maxHealth);
        Debug.Log("Add health = " + health + " , current health = " + _currentHealth);
    }
    private void BasicAttack()
    {
        audioManagerScript.SoundEffect(audioManagerScript.Attack);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            if(enemy.GetComponentInChildren<EnemyScript>()!=null){
                enemy.GetComponentInChildren<EnemyScript>().TakeDamage(_attackDamage);
            }
            if(enemy.GetComponentInChildren<Boss_1>()!=null){
                enemy.GetComponentInChildren<Boss_1>().TakeDamage(_attackDamage);
            }

        }
    }
    private void DartAttack()
    {
        Instantiate(DartObject, transform.position, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    #endregion

    #region Move
    private void checkOnGround()
    {
        bool ground = false;
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 2f, groundLayer);
        Color rayColor = hit.collider != null ? Color.red : Color.blue;
        Debug.DrawRay(rayOrigin, Vector2.down * 2f, rayColor);
        if (hit.collider != null)
        {
            ground = true;
        }
        isGrounded = ground;
    }
    private void Move()
    {
        if (isGrounded && moveVertical > 0.1f)
        {
            audioManagerScript.SoundEffect(audioManagerScript.Jump);
            DustEffect();
            rb.velocity = (new Vector2(rb.velocity.x, 1 * jumpForce));
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

    // Combat
    private static readonly int AT1 = Animator.StringToHash("Player_AT1");
    private static readonly int AT2 = Animator.StringToHash("Player_AT2");
    private static readonly int TakeHit = Animator.StringToHash("Player_TakeHit");
    private static readonly int Dart = Animator.StringToHash("phi_tieu");
    private string[] combatAnimations = { "Player_TakeHit", "Player_AT1", "Player_AT2", "phi_tieu" };
    #endregion


}