using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMoverment : MonoBehaviour
{

    private bool playIsDead;
    private GameObject player;
    private EnemyScript enemyScript;
    private PlayerMoverment playerMoverment;
    private new Collider2D collider;
    public Rigidbody2D rb;
    [SerializeField] private float distanceX;
    private float distanceY, deltaX, deltaY;
    private float speed;
    private float distance, direction;
    private bool isFacingRight = true;
    private bool isRunning = false;
    private float length;

    public float getDistance()
    {
        return distance;
    }
    public float getDistanceX()
    {
        return distanceX;
    }

    private void Awake()
    {
        enemyScript = GetComponentInChildren<EnemyScript>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        length = GetComponent<Collider2D>().bounds.size.x;
        playerMoverment = FindObjectOfType<PlayerMoverment>();
        collider = GetComponent<Collider2D>();
        speed = 2.5f;
    }
    public bool getIsRunning()
    {
        return isRunning;
    }

    private void FixedUpdate()
    {

        if (enemyScript.getCurrentHealth() == 0)
        {
            collider.enabled = false;
            Destroy(rb);
            return;
        }
        distanceY = distanceX / 2;
        Vector2 objectPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        deltaX = Mathf.Abs(objectPosition.x - playerPosition.x);

        deltaY = Mathf.Abs(objectPosition.y - playerPosition.y);

        if ((deltaX * deltaX) / (distanceX * distanceX) + (deltaY * deltaY) / (distanceY * distanceY) <= 1)
        {
            //if (playerMoverment.getDead()) return;
            isRunning = true;
            Vector2 target = new Vector2(playerPosition.x, objectPosition.y);


            direction = objectPosition.x < playerPosition.x ? 1 : -1;

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            if (objectPosition.x < playerPosition.x && isFacingRight)
            {
                Flip();
            }
            else if (objectPosition.x > playerPosition.x && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            isRunning = false;
            float deceleration = 1.5f;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime), rb.velocity.y);
        }

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.rotation = Quaternion.Euler(new Vector3(0, isFacingRight ? 0 : 180, 0));
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        bool x = true;
    //        playerMoverment.setDead(x);
    //    }
    //}

}
