using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGarenScript : MonoBehaviour
{
    private Collider2D collider;
    public int _attackDamage = 40;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<EnemyScript>() != null)
        {
            collision.GetComponentInChildren<EnemyScript>().TakeDamage(_attackDamage);
        }
    }

}
