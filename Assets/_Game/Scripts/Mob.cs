using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : MonoBehaviour, IDieable
{

    private bool playIsDead;
    private GameObject player;
    private PlayerMoverment playerMoverment;
    public Rigidbody2D rb;
    public LayerMask layerMark;
    [SerializeField] private float distanceX;
    private float distanceY, deltaX, deltaY;
    private float speed;
    private float distance, direction;
    private bool isFacingRight = true;
    private bool isDead = false, isRunning = false;
    private float length;

    public float getDistance()
    {
        return distance;
    }
    public float getDistanceX()
    {
        return distanceX;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        length = GetComponent<Collider2D>().bounds.size.x;
        playerMoverment = FindObjectOfType<PlayerMoverment>();
        speed = 500f;
    }
    public bool getIsRunning()
    {
        return isRunning;
    }
    public void setDead(bool isDead)
    {
        this.isDead = isDead;
    }
    public bool getDead()
    {
        return isDead;
    }

    private void FixedUpdate()
    {

        if (isDead) return;
        distanceY = distanceX / 2;
        Vector2 objectPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        deltaX = Mathf.Abs(objectPosition.x - playerPosition.x);

        deltaY = Mathf.Abs(objectPosition.y - playerPosition.y);

        if ((deltaX * deltaX) / (distanceX * distanceX) + (deltaY * deltaY) / (distanceY * distanceY) <= 1)
        {
            if (playerMoverment.getDead()) return;
            isRunning = true;
            Vector2 target = new Vector2(playerPosition.x, objectPosition.y);


            direction = objectPosition.x < playerPosition.x ? 1 : -1;

            rb.velocity = new Vector2(direction * Time.fixedDeltaTime * speed, rb.velocity.y);
            if (objectPosition.x < playerPosition.x && !isFacingRight)
            {
                Flip();
            }
            else if (objectPosition.x > playerPosition.x && isFacingRight)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool x = true;
            playerMoverment.setDead(x);
        }
    }

}
