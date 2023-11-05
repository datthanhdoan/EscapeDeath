using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleFall : KillPlayerScript
{
    public LayerMask targetLayer;
    private Rigidbody2D rb;
    [SerializeField] private float raycastDistance = 15f;
    private float timer = 0f;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 raycastOrigin = transform.position;
        Vector2 raycastDirection = Vector2.down;



        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, targetLayer);

        Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red);

        if (hit.collider != null)
        {
            Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.blue);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 10.0f;

        }
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                Destroy(gameObject);
            }
        }

    }
}
