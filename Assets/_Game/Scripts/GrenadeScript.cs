using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private GameObject player;
    private EnemyMoverment EnemyMoverment;
    private Rigidbody2D rb;
    [SerializeField] private float force = 25;
    private float timer;
    [SerializeField] private float _lifetime = 1.2f;
    private PlayerMoverment playerMoverment;
    [SerializeField] private GameObject effect;
    private AudioManagerScript audioManagerScript;

    private void Awake()
    {

        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();

    }
    // Start is called before the first frame update
    void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
        timer = 0;
        EnemyMoverment = FindObjectOfType<EnemyMoverment>();
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
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManagerScript.SoundEffect(audioManagerScript.Grenade);
            Instantiate(effect, transform.position, Quaternion.identity);
            playerMoverment.setDead(true);
        }
        if (collision.gameObject.CompareTag("EnemyMoverment"))
        {
            //EnemyMoverment.setDead(true);
        }
    }
}
