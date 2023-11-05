using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private GameObject player;
    private Mob mob;
    private Rigidbody2D rb;
    [SerializeField] private float force = 25;
    private float timer;
    [SerializeField] private float _lifetime = 1.2f;
    private PlayerMoverment playerMoverment;

    // Start is called before the first frame update
    void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
        timer = 0;
        mob = FindObjectOfType<Mob>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > _lifetime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMoverment.setDead(true);
        }
        if (collision.gameObject.CompareTag("Mob"))
        {
            mob.setDead(true);
        }
    }
}
