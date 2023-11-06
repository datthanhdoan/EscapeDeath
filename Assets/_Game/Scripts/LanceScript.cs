using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceScript : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
