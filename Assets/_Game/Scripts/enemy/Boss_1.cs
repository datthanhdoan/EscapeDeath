using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _anim;
    public EnemyMoverment _enemyMoverment;
    public int maxHealth = 30;
    private int currentHealth;
    public HealthBar healthBar;
    [Header("Attack")]
    public Transform _attackPoint;
    public int _attackDamage = 10;
    public bool isCombat = false;
    public bool isHurt = false;
    [Range(0, 5)] public float _attackRange;
    [SerializeField] private LayerMask _playerLayers;
    private PlayerMoverment _playerMoverment;
    public GameObject FloatingPoint;
    public GameObject heartDrop;
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth, maxHealth);
        _playerMoverment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoverment>();
    }

    // Update is called once per frame
    public void Attack1()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayers);
        foreach (Collider2D hit in hitPlayer)
        {
            Debug.Log("Enemy Attack player");
            hit.GetComponent<PlayerMoverment>().TakeDamage(_attackDamage);
        }
    }

    public void HurtDone()
    {
        _anim.SetBool("Hurt", false);
        isCombat = false;
        isHurt = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        //_anim.SetBool("Hurt", true);
        isCombat = true;
        isHurt = true;
        GameObject points = Instantiate(FloatingPoint, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (currentHealth <= 0)
        {
            if (heartDrop != null)
            {
                Instantiate(heartDrop, transform.position, Quaternion.identity);

            }
            _anim.SetBool("isDead", true);
            _playerMoverment.points += 100;
            this.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
