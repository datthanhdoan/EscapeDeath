using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    // Start is called before the first frame update\
    private AudioManagerScript audioManagerScript;
    private Rigidbody2D rb;
    [SerializeField] private int addHealth = 5;
    private float _forceY = 10f;
    private float _forceX;
    public GameObject healthEffect;
    public bool _gravity;

    void Start()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        if (_gravity)
        {
            _forceX = Random.Range(0, 1) == 0 ? -1f : 1f;
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(_forceX, _forceY);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManagerScript.SoundEffect(audioManagerScript.Heal);
            collision.GetComponent<PlayerMoverment>().TakeHealth(addHealth);
            Instantiate(healthEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
