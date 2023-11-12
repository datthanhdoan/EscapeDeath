using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _anim;
    public EnemyMoverment _enemyMoverment;
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;
    public HealthBar healthBar;
    [Header("Attack")]
    public Transform _attackPoint;
    public int _attackDamage = 10;
    public bool isCombat = false;
    public bool isHurt = false;
    [Range(0, 5)] public float _attackRange;
    [SerializeField] private LayerMask _playerLayers;

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
    }

    // Update is called once per frame
    void Update()
    {
        //if (_anim.GetBool("Hurt"))
        //{
        //    _enemyMoverment.setIsRunning(false);
        //}
        if (_enemyMoverment.getIsRunning() && currentHealth > 0)
        {
            _anim.SetBool("Run", true);
        }
        else
        {
            _anim.SetBool("Run", false);
        }
    }
    public void Attack()
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
        _anim.SetBool("Hurt", true);
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
            this.enabled = false;
        }
    }
}
