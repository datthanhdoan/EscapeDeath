using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTrap : Trap
{
    private Animator _anim;
    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_timerDelayAttack < 0)
            {
                _anim.CrossFade(WormAttack, 0, 0);
            }
        }
    }
    private static readonly int WormAttack = Animator.StringToHash("Worm_Attack");
}


