using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bearTrap : Trap
{
    private Animator anim;
    private Collider2D col;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    private static readonly int BearTrapClose = Animator.StringToHash("BearTrap_Close");

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        anim.CrossFade(BearTrapClose, 0, 0);
        col.enabled = false;
    }
}
