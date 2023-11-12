using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 40f;
    public GameObject takeDartEffect;
    private GameObject player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {

        // Lấy hướng di chuyển từ trục quay Y của đối tượng cha
        Vector2 moveDirection = player.transform.TransformDirection(Vector2.right);

        // Đặt tốc độ của đối tượng
        rb.velocity = moveDirection * speed;

        // Di chuyển đối tượng ra khỏi đối tượng cha một khoảng cố định
        float playerRotationY = player.transform.eulerAngles.y;
        float dartPosWithPlayer = playerRotationY == 0 ? 2f : -2f;
        transform.localPosition += new Vector3(dartPosWithPlayer, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponentInChildren<EnemyScript>().TakeDamage(10);
            Instantiate(takeDartEffect, transform.position, Quaternion.identity);
        }
        if (!collision.gameObject.CompareTag("Health"))
        {
            Destroy(gameObject);
        }
    }

}

