using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 40f;
    public GameObject takeArrowEffect;
    private GameObject player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {


        // Đặt tốc độ của đối tượng
        rb.velocity = Vector2.left * speed;

        // Di chuyển đối tượng ra khỏi đối tượng cha một khoảng cố định
        //float playerRotationY = player.transform.eulerAngles.y;
        //float dartPosWithPlayer = playerRotationY == 0 ? 2f : -2f;
        //transform.localPosition += new Vector3(dartPosWithPlayer, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInChildren<PlayerMoverment>().TakeDamage(10);
            Instantiate(takeArrowEffect, transform.position, Quaternion.identity);
        }
        if (!collision.gameObject.CompareTag("Health"))
        {
            Destroy(gameObject);
        }
    }

}

