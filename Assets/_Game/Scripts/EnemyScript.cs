using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _anim;
    private int maxHealth = 30;
    private int currentHealth;
    public HealthBar healthBar;
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

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        _anim.SetTrigger("Hurt");
        if (currentHealth == 0)
        {
            _anim.SetBool("isDead", true);
            this.enabled = false;
        }
    }
}
