using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : KillPlayerScript
{
    private CapsuleCollider2D capsu;
    protected override void Start()
    {
        base.Start();
        capsu = GetComponent<CapsuleCollider2D>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // Kiểm tra Rock gameObject
        if (collision.gameObject.CompareTag("Lava"))
        {
            capsu.enabled = false;
        }
    }
}
